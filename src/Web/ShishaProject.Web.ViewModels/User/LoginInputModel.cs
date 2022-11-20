namespace ShishaProject.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class LoginInputModel
    {
        [JsonProperty("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
    }
}
