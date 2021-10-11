namespace ShishaProject.Services.Data.Models.Filters
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Filters
    {
        [JsonProperty("price_from")]
        public string PriceFrom { get; set; }

        [JsonProperty("price_to")]
        public string PriceTo { get; set; }

        [JsonProperty("in_stock")]
        public bool InStock { get; set; }

        [JsonProperty("category_id")]
        public IEnumerable<int> CategoryId { get; set; }
    }
}
