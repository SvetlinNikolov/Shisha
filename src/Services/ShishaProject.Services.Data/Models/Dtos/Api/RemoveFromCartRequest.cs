namespace ShishaProject.Services.Data.Models.Dtos.Api
{
    using Newtonsoft.Json;

    public class RemoveFromCartRequest
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "quantity_remove")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "flavour_variation_id")]
        public int FlavourVariationId { get; set; }

        [JsonProperty(PropertyName = "flavour_id")]
        public int FlavourId { get; set; }
    }
}
