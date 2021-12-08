﻿namespace ShishaProject.Services
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
    using ShishaProject.Common.ExceptionHandling;
    using ShishaProject.Common.Helpers;
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

        public UsersService(
            IRestClient restClient,
            IOptions<UsersEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContext,
            IShishaLogger logger,
            IEmailService emailService)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContext;
            this.logger = logger;
            this.emailService = emailService;
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
            string request = this.emailService.IsValidEmail(usernameOrEmail) ? "email" : "username";

            try
            {
                var result = await this.restClient
              .PostAsync<JObject>(
               this.endpointConfig.Value.GetUserByUsernameOrEmail,
               JsonHelper.SerializeToPhpApiFormat(request, usernameOrEmail));

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

        public async Task<bool> AuthenticateUser(LoginInputModel model)
        {
            bool isAuthenticated;

            try
            {
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

        public async Task<bool> RegisterUserAsync(RegistrationInputModel user)
        {
            user.City = "Ne sam go napravil o6te";
            var result = await this.restClient
                 .PostAsync<UserDto>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", user));

            return !string.IsNullOrEmpty(result.UserId);
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

        public async Task LogoutUser()
        {
            if (this.UserLoggedIn())
            {
                await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
