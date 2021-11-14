namespace ShishaProject.Web.ViewModels.Users
{
    using Newtonsoft.Json;

    public class LoginInputModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
