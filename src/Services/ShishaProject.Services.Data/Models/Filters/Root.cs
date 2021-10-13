namespace ShishaProject.Services.Data.Models.Filters
{
    using Newtonsoft.Json;

    public class Root
    {
        [JsonProperty("data")]
        public FilterData Data { get; set; }
    }
}
