namespace ShishaProject.Services.Data.Models.Dtos.Api
{

    using Newtonsoft.Json;
    using ShishaProject.Services.Data.Enums;

    public class RelatedFlavoursRequest
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("flavour_type")]
        public FlavourType? FlavourType { get; set; }
    }
}
