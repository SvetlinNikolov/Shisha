namespace ShishaProject.Services.Data.Models.Configs
{
    public class UsersEndpointsConfig
    {
        public string GetAllUsers { get; set; }

        public string GetUserById { get; set; }

        public string GetUserByEmail { get; set; }

        public string RegisterUser { get; set; }

        public string UpdateUser { get; set; }

        public string AuthenticateUser { get; set; }

        public string GetUserByEmailToken { get; set; }
    }
}
