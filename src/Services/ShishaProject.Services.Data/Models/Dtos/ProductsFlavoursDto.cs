namespace ShishaProject.Services.Data.Models.Dtos
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ProductsFlavoursDto
    {
        [JsonProperty("data")]
        public IEnumerable<ProductFlavourDto> Flavours { get; set; }
    }
}
