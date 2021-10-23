namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class GetAllFlavoursRequest
    {
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
