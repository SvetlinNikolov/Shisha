namespace ShishaProject.Services.Data.Models.Filters
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FilterData
    {
        public string currentPageNumber { get; set; }
        public string selectValue { get; set; }
        public string price_from { get; set; }
        public string price_to { get; set; }
        public IEnumerable<object> category_id { get; set; }
        //public List<object> svetlio { get; set; }
        //public List<object> svetlio2 { get; set; }
        public bool in_stock { get; set; }
        public string language { get; set; }
    }
}
