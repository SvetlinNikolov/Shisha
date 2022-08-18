namespace ShishaProject.Services.Data.Models.Dtos
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ShishaProject.Services.Data.Enums;

    public class ProductFlavourDto
    {
        public ProductFlavourDto()
        {
            this.RelatedFlavours = new List<ProductFlavourDto>();
            this.Variations = new List<FlavourVariation>();
            this.Images = new List<string>();
        }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("category_id")] public int CategoryId { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("price")] public decimal Price { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("short_description")] public string ShortDescription { get; set; }

        [JsonProperty("image")] public string Thumbnail { get; set; }

        [JsonProperty("image_gallery")] public IEnumerable<string> Images { get; set; }

        [JsonProperty("created_at")] public string CreatedAt { get; set; }

        [JsonProperty("updated_at")] public string UpdatedAt { get; set; }

        [JsonProperty("flavour_type")] public FlavourType? FlavourType { get; set; }

        [JsonProperty("flavour_variations")] public IEnumerable<FlavourVariation> Variations { get; set; }

        [JsonProperty("related_products")] public IEnumerable<ProductFlavourDto> RelatedFlavours { get; set; }
    }
}