namespace ShishaProject.Services.Data.Models.Dtos
{
    using Newtonsoft.Json;

    public class ShishaResponseDto<T> : ShishaResponseDto
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class ShishaResponseDto
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("error_message")]
        public string Errors { get; set; }
    }
}
