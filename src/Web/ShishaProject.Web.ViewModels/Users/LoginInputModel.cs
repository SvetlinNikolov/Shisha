namespace ShishaProject.Web.ViewModels.Users
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [JsonProperty("username")]
        [Required]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
    }
}
