namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ShishaProject.Services.Interfaces;

    public class UsersService : IUserService
    {
        private const string endpoint = "users/register-user";
        private readonly IRestClient restClient;

        public UsersService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task RegisterUser(dynamic user)
        {
            var myModel = new RegisterModel
            {
                Address = "na ulicata",
                City = "flowers",
                CreatedAt = DateTime.Now.ToString(),
                Email = "daxtera@abv.bg",
                FirstName = "dasx",
                LastName = "peshov",
                Password = "super heshirana parola",
                Username = "daxterr"
            };

            var arr = new RegisterModelArr { UserData = myModel };

            var result = await this.restClient
                .PostAsync<dynamic>(endpoint, JsonConvert.SerializeObject(arr));
        }
    }
    class RegisterModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
    class RegisterModelArr
    {
        [JsonProperty("user_data")]
        public RegisterModel UserData { get; set; }
    }
}
