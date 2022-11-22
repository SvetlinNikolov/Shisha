namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;
    using ShishaProject.Common;

    public class SearchFlavourRequest
    {
        [JsonProperty("term")]
        [JsonRequired]
        public string SearchQuery { get; set; }

        [JsonProperty("per_page")]
        public int ResultsPerPage { get; } = GlobalConstants.DefaultItemsPerPage;

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
    }
}
