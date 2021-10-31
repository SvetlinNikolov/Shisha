namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class GetAllFlavoursRequest
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("items_per_page")]
        public int FlavoursPerPage => 1;
    }
}
