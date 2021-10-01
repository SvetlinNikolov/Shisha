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

        public async Task<bool> AuthenticateUser(UserDto model)
        {
            var myModel = new UserDto
            {
                Password = "super heshirana parola",
                Username = "daxterr1123",
            };

            var result = await this.restClient
              .PostAsync<UserDto>(this.endpointConfig.Value.AuthenticateUser, JsonConvert.SerializeObject(myModel));

            return true;
        }

        public async Task<bool> RegisterUserAsync(RegistrationInputModel user)
        {
            var myModel = new RegistrationInputModel
            {
                Address = "na ulicata",
                City = "flowers",
                CreatedAt = DateTime.Now,
                Email = "daxtera1123@abv.bg",
                FirstName = "das123x11",
                LastName = "pe123shov",
                Password = "super heshirana parola",
                Username = "daxterr1123",
                PhoneNumber = "0876444918",
            };

            var result = await this.restClient
                .PostAsync<UserDto>(this.endpointConfig.Value.RegisterUser, JsonHelper.SerializeToPhpApiFormat("user_data", myModel));

            return true;
        }
    }
}
