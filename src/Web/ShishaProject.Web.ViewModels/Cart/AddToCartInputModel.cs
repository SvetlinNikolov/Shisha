namespace ShishaProject.Web.ViewModels.Cart
{
    using Newtonsoft.Json;

    public class AddToCartInputModel
    {
        [JsonProperty("flavour_id")]
        [JsonRequired]
        public int FlavourId { get; set; }

        [JsonProperty("flavour_variation_id")]
        [JsonRequired]
        public int VariationId { get; set; }

        [JsonProperty("quantity")]
        [JsonRequired]
        public int Quantity { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }
}
