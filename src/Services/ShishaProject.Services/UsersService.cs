namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Policy;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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

        public UsersService(
            IRestClient restClient,
            IOptions<UsersEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContext,
            IShishaLogger logger,
            IEmailService emailService,
            IUserSecurityService userSecurityService)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContext;
            this.logger = logger;
            this.emailService = emailService;
            this.userSecurityService = userSecurityService;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                var result = await this.restClient
                .PostAsync<JObject>(
                 this.endpointConfig.Value.GetUserById,
                 JsonHelper.SerializeToPhpApiFormat("user_id", id));

                var user = result.Value<JObject>("data")
                    .ToObject(typeof(UserDto)) as UserDto;

                return user;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                return null;
            }
        }

        public async Task<UserDto> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var result = await this.restClient
              .PostAsync<JObject>(
               this.endpointConfig.Value.GetUserByUsernameOrEmail,
               JsonHelper.SerializeToPhpApiFormat("username_or_email", usernameOrEmail));

                var user = result.Value<JObject>("data")
                    .ToObject(typeof(UserDto)) as UserDto;

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
                model.Password = this.userSecurityService.EncryptPassword(model.Password).Password; //FIX THIS

                var result = await this.restClient
                             .PostAsync<JObject>(this.endpointConfig.Value.AuthenticateUser, JsonConvert.SerializeObject(model));

                var user = result.Value<JObject>("data")
                                 .ToObject(typeof(UserDto)) as UserDto;

                isAuthenticated = !string.IsNullOrEmpty(user.UserId);
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
                 .PostAsync<JObject>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", user));

            var userDto = result.Value<JObject>("data")
               .ToObject(typeof(UserDto)) as UserDto;

            var userRegistered = !string.IsNullOrEmpty(userDto.UserId); // Better to check status code from result

            if (userRegistered)
            {
                return userDto;
            }

            return null;
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
                    new Claim(ClaimTypes.Name, inputModel.UsernameOrEmail),
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

                    var user = await this.GetUserByUsernameOrEmailAsync(usernameOrEmail);

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
                user.ConfirmEmail = true;
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
                     .PutAsync<ShishaResponseDto<string>>(
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
                     .PutAsync<ShishaResponseDto<string>>(
                         this.endpointConfig.Value.UpdateUser,
                         JsonHelper.SerializeToPhpApiFormat("user_data", userModel));
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                throw;
            }
        }

        private void SetPasswordAndSalt(RegistrationInputModel user)
        {
            var (password, salt) = this.userSecurityService.EncryptPassword(user.Password);

            user.Password = password;
            user.Salt = salt;
        }
    }
}
