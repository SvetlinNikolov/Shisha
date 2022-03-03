namespace ShishaProject.Services.Data.Models.Dtos
{
    using Newtonsoft.Json;

    public class ConfirmUserEmailDto
    {
        [JsonProperty("id")]
        public int UserId { get; set; }

        [JsonProperty("confirmed_email")]
        public bool ConfirmEmail { get; set; }
    }
}
