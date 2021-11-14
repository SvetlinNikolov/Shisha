namespace ShishaProject.Services
{
    using System;
    using System.Threading.Tasks;

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

        public UsersService(
            IRestClient restClient,
            IOptions<UsersEndpointsConfig> endpointConfig)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var result = await this.restClient
                .PostAsync<JObject>(
                 this.endpointConfig.Value.GetUserById,
                 JsonHelper.SerializeToPhpApiFormat("user_id", id));

            var user = result.Value<JObject>("data")
                .ToObject(typeof(UserDto)) as UserDto;

            return user;
        }

        public async Task<bool> AuthenticateUser(LoginInputModel model)
        {
            var result = await this.restClient
              .PostAsync<UserDto>(this.endpointConfig.Value.AuthenticateUser, JsonConvert.SerializeObject(model));

            return result != null;
        }

        public async Task<bool> RegisterUserAsync(RegistrationInputModel user)
        {
            user.City = "Ne sam go napravil o6te";
            var result = await this.restClient
                 .PostAsync<UserDto>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", user));

            return result != null;
        }
    }
}
