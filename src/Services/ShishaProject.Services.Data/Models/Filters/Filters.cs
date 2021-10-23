namespace ShishaProject.Services.Data.Models.Filters
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Filters
    {
        [JsonProperty("currentPageNumber")]
        public string CurrentPageNumber { get; set; }

        [JsonProperty("selectValue")]
        public string SortBy { get; set; }

        [JsonProperty("price_from")]
        public string PriceFrom { get; set; }

        [JsonProperty("price_to")]
        public string PriceTo { get; set; }

        [JsonProperty("category_id")]
        public IEnumerable<int> CategoryIds { get; set; }

        [JsonProperty("in_stock")]
        public short InStock { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("items_per_page")]
        public int FlavoursPerPage => 9;
    }
}
