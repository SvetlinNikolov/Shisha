namespace ShishaProject.Services.Data.Models.Filters
{

    using Newtonsoft.Json;

    public class Data
    {
        public string currentPageNumber { get; set; }
        public string selectValue { get; set; }
        public string price_from { get; set; }
        public string price_to { get; set; }
        public int category_id { get; set; }

        public bool in_stock { get; set; }

        [JsonProperty("language")]  
        public string Language{ get; set; }
    }
}
