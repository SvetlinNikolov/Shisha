namespace ShishaProject.Services.Data.Models.Dtos
{
    using System;

    using Newtonsoft.Json;

    public class FlavourVariation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("flavour_id")]
        public int FlavourId { get; set; }
    }
}
