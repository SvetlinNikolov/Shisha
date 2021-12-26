namespace ShishaProject.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class LoginInputModel
    {
        [JsonProperty("username_or_email")]
        [Required]
        public string UsernameOrEmail { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
    }
}
