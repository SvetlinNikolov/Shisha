namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Claims;
    using System.Security.Policy;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ShishaProject.Common.Caching;
    using ShishaProject.Common.Constants;
    using ShishaProject.Common.ExceptionHandling;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Common.Utils;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.User;

    public class UsersService : IUsersService
    {
        private readonly IRestClient restClient;
        private readonly IOptions<UsersEndpointsConfig> endpointConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IShishaLogger logger;
        private readonly IEmailService emailService;
        private readonly IUserSecurityService userSecurityService;
        private readonly IShishaCache shishaCache;

        public UsersService(
            IRestClient restClient,
            IOptions<UsersEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContext,
            IShishaLogger logger,
            IEmailService emailService,
            IUserSecurityService userSecurityService,
            IShishaCache shishaCache)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContext;
            this.logger = logger;
            this.emailService = emailService;
            this.userSecurityService = userSecurityService;
            this.shishaCache = shishaCache;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (this.shishaCache.TryGet<UserDto>(id.ToString(), out var cachedUser))
            {
                return cachedUser;
            }

            try
            {
                var result = await this.restClient
                           .PostAsync<ShishaResponseDto<UserDto>>(
                            this.endpointConfig.Value.GetUserById,
                            JsonHelper.SerializeToPhpApiFormat("user_id", id));

                var user = result.Data;

                if (user != null)
                {
                    this.shishaCache.SetOrUpdate<UserDto>(id.ToString(), user);
                }

                return user;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                return null;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                if (this.shishaCache.TryGet<UserDto>(email, out var cachedUser))
                {
                    return cachedUser;
                }

                var result = await this.restClient
                            .PostAsync<ShishaResponseDto<UserDto>>(
                             this.endpointConfig.Value.GetUserByEmail,
                             JsonHelper.SerializeToPhpApiFormat("email", email));

                var user = result.Data;

                if (user != null)
                {
                    this.shishaCache.SetOrUpdate<UserDto>(email, user);
                }

                return user;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> AuthenticateUserAsync(LoginInputModel model)
        {
            bool isAuthenticated;

            try
            {
                var user = await this.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    return isAuthenticated = false;
                }

                model.Password = this.userSecurityService.EncryptPassword(model.Password, user.Salt).HashedPassword;

                var result = await this.restClient
                             .PostAsync<ShishaResponseDto<UserDto>>(this.endpointConfig.Value.AuthenticateUser, JsonConvert.SerializeObject(model));

                isAuthenticated = result?.Data != null;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                isAuthenticated = false;
            }

            return isAuthenticated;
        }

        public async Task<UserDto> RegisterUserAsync(RegistrationInputModel user)
        {
            this.SetPasswordAndSalt(user);

            var result = await this.restClient
                 .PostAsync<ShishaResponseDto<UserDto>>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", user));

            return result?.Data;
        }

        public bool UserLoggedIn()
        {
            bool isCurrentlyLoggedIn = (this.httpContextAccessor.HttpContext.User != null) &&
                this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            return isCurrentlyLoggedIn;
        }

        public async Task LoginUserAsync(LoginInputModel inputModel)
        {
            // the null check below is a bit redundant but lets keep it for now
            if (!this.UserLoggedIn())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, inputModel.Email),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await this.httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            else
            {
                throw new InvalidOperationException("User already logged in");
            }
        }

        public async Task LogoutUserAsync()
        {
            if (this.UserLoggedIn())
            {
                var user = await this.GetLoggedInUserAsync();
                this.shishaCache.RemoveFromCache<UserDto>(user.Email); //this will only work if we only let users login with their email

                await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        public async Task<UserDto> GetLoggedInUserAsync()
        {
            try
            {
                if (this.UserLoggedIn())
                {
                    var usernameOrEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;

                    var user = await this.GetUserByEmailAsync(usernameOrEmail);

                    if (user == null)
                    {
                        throw new ArgumentException($"User with username or email {usernameOrEmail} does not exist");
                    }

                    return user;
                }

                throw new InvalidOperationException("User not logged in");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                return null;
            }
        }

        //Get
        public Task ResetUserPasswordAsync(string passwordToken)
        {
            throw new NotImplementedException();
            //var user = this.GetUserByPasswordResetToken(passwordToken);
        }

        public async Task<bool> UpdateUserConfirmedEmailAsync(string confirmEmailToken)
        {
            var result = await this.restClient
                      .PostAsync<ShishaResponseDto<UserDto>>(
                          this.endpointConfig.Value.GetUserByEmailToken,
                          JsonHelper.SerializeToPhpApiFormat("email_token", confirmEmailToken));

            var user = result.Data;

            if (user.ConfirmEmail == false)
            {
                await this.ConfirmUserEmail(new ConfirmUserEmailDto { ConfirmEmail = true, UserId = user.UserId });

                return true;
            }

            return false;
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            try
            {
                var result = await this.restClient
                     .PutAsync<ShishaResponseDto>(
                         this.endpointConfig.Value.UpdateUser,
                         JsonHelper.SerializeToPhpApiFormat("user_data", userDto));
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                throw;
            }
        }

        public async Task ConfirmUserEmail(ConfirmUserEmailDto userModel)
        {
            try
            {
                var result = await this.restClient
                     .PutAsync<ShishaResponseDto>(
                         this.endpointConfig.Value.UpdateUser,
                         JsonHelper.SerializeToPhpApiFormat("user_data", userModel));
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                throw;
            }
        }

        public async Task<int> GetLoggedInUserIdAsync()
        {
            var currentUser = await this.GetLoggedInUserAsync();
            int.TryParse(currentUser?.UserId, out int userId);

            if (userId == 0)
            {
                throw new Exception($"Invalid user id {userId}");
            }

            return userId;
        }

        private void SetPasswordAndSalt(RegistrationInputModel user)
        {
            var (password, salt) = this.userSecurityService.EncryptPassword(user.Password);

            user.Password = password;
            user.Salt = salt;
        }
    }
}
