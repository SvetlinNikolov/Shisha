namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class FlavourByCategoryIdRequest
    {
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
