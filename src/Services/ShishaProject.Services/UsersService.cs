namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IRestClient restClient;
        private readonly IOptions<UsersEndpointsConfig> endpointConfig;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersService(
            IRestClient restClient,
            IOptions<UsersEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContext)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContext;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var result = await this.restClient
                .PostAsync<JObject>(
                 this.endpointConfig.Value.GetUserById,
                 JsonHelper.SerializeToPhpApiFormat("user_id", id));

            var user = result.Value<JObject>("data")
                .ToObject(typeof(UserDto)) as UserDto;
            // tuka ako nqma su6to trqbva da grumne

            return user;
        }

        public async Task<bool> AuthenticateUser(LoginInputModel model)
        {
            bool isAuthenticated;

            try
            {
                var result = await this.restClient
                             .PostAsync<JObject>(this.endpointConfig.Value.AuthenticateUser, JsonConvert.SerializeObject(model));

                var user = result.Value<JObject>("data")
                                 .ToObject(typeof(UserDto)) as UserDto;

                isAuthenticated = user.Username != null;
            }
            catch (Exception ex)
            {
                //log exception
                isAuthenticated = false;
            }

            return isAuthenticated;
        }

        public async Task<bool> RegisterUserAsync(RegistrationInputModel user)
        {
            user.City = "Ne sam go napravil o6te";
            var result = await this.restClient
                 .PostAsync<UserDto>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", user));

            return result != null;
        }

        public bool UserLoggedIn()
        {
            bool isCurrentlyLoggedIn = (this.httpContextAccessor.HttpContext.User != null) &&
                this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            return isCurrentlyLoggedIn;
        }

        public async Task LoginUser(LoginInputModel inputModel)
        {
            // the null check below is a bit redundant but lets keep it for now
            if (!this.UserLoggedIn())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, inputModel.Username),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await this.httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            else
            {
                throw new InvalidOperationException("User already logged in");
            }
        }

        public async Task LogoutUser()
        {
            if (this.UserLoggedIn())
            {
                await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
