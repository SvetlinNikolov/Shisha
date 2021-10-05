namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class FlavourByIdRequest
    {
        [JsonProperty("id")]
        public int FlavourId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
