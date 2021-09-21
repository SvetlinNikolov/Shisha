namespace ShishaProject.Services.Data.Models.Dtos
{
    using Newtonsoft.Json;

    public class ProductCategoryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parent_id")]
        public int ParentId { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
