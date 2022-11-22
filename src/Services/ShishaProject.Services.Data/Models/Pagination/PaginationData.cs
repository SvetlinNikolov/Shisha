namespace ShishaProject.Services.Data.Models.Pagination
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using ShishaProject.Common;

    public class PaginationData
    {
        public PaginationData()
        {
            this.Pages = new List<int>();
        }

        [JsonProperty("last_page")]
        public int TotalPages { get; set; }

        [JsonProperty("total_products")]
        public int TotalProducts { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; } = 1;

        [JsonProperty("per_page")]
        public int ItemsPerPage { get; set; } = GlobalConstants.DefaultItemsPerPage;

        [JsonIgnore]
        public IEnumerable<int> Pages { get; set; }
    }
}
