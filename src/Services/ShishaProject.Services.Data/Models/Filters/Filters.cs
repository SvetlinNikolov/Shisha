namespace ShishaProject.Services.Data.Models.Filters
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Filters
    {
        [JsonProperty("page")]
        public string CurrentPageNumber { get; set; }

        [JsonProperty("selectValue")]
        public string SortBy { get; set; }

        [JsonProperty("price_from")]
        public decimal? PriceFrom { get; set; }

        [JsonProperty("price_to")]
        public decimal? PriceTo { get; set; }

        [JsonProperty("category_id")]
        public IEnumerable<int> CategoryIds { get; set; }

        [JsonProperty("in_stock")]
        public short InStock => 1;

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("items_per_page")]
        public int FlavoursPerPage => 1;
    }
}
