namespace ShishaProject.Services.Data.Models.Filters
{
    using Newtonsoft.Json;

    public class FilterData
    {
        [JsonProperty("data")]
        public FilterContext Kur { get; set; }
    }
}
