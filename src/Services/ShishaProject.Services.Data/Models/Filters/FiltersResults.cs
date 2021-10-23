namespace ShishaProject.Services.Data.Models.Filters
{
    using Newtonsoft.Json;
    using ShishaProject.Services.Data.Models.Dtos;

    public class FiltersResults
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("items")]
        public ProductsFlavoursDto Results { get; set; }
    }
}
