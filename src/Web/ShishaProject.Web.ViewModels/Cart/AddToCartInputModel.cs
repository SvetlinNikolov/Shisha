using Newtonsoft.Json;

namespace ShishaProject.Web.ViewModels.Cart
{
    public class AddToCartInputModel
    {
        public int FlavourId { get; set; }

        [JsonProperty("variationId")]
        public int VariationId { get; set; }

        public int Quantity { get; set; }
    }
}
