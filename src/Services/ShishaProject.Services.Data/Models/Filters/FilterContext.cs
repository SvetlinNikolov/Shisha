namespace ShishaProject.Services.Data.Models.Filters
{

    using Newtonsoft.Json;

    public class FilterContext
    {
        [JsonProperty("currentPageNumber")]
        public int CurrentPage { get; set; }

        [JsonProperty("selectValue")]
        public string SortBy { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }
    }
}
