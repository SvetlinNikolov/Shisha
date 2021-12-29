namespace ShishaProject.Services.Data.Models.Dtos
{
    using Newtonsoft.Json;

    public class ShishaResponseDto
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("error_message")]
        public string Errors { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
