namespace ShishaProject.Services.Data.Models.Dtos
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ProductsFlavoursDto
    {
        [JsonProperty("status")]
        public int StatusCode { get; set; }

        [JsonProperty("data")]
        public IEnumerable<ProductFlavourDto> Flavours { get; set; }
    }
}
