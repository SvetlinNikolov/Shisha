namespace ShishaProject.Services.Data.Models.Filters
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Filters
    {
        public string price_from { get; set; }
        public string price_to { get; set; }
        public List<object> category_id { get; set; }
        public List<object> svetlio { get; set; }
        public List<object> svetlio2 { get; set; }
        public bool in_stock { get; set; }
    }
}
