namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class GetProductFlavourRequest
    {
        [JsonProperty("id")]
        public int FlavourId { get; set; }
    }
}
