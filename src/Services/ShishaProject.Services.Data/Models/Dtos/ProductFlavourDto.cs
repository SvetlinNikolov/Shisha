namespace ShishaProject.Services.Data.Models.Dtos
{

    using Newtonsoft.Json;

    public class ProductFlavourDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        public string Image { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
    }
}
