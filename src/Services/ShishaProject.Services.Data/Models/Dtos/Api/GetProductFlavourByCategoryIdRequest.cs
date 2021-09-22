namespace ShishaProject.Services.Data.Models.Dtos.Api
{

    using Newtonsoft.Json;

    public class GetProductFlavourByCategoryIdRequest
    {
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
    }
}
