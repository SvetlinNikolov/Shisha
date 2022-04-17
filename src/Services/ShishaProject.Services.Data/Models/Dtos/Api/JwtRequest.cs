namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class JwtRequest
    {
        [JsonProperty("jwt_key")]
        public string Key { get; set; }
    }
}
