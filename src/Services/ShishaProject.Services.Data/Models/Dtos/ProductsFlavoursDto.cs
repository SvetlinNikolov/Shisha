namespace ShishaProject.Services.Data.Models.Dtos
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using ShishaProject.Services.Data.Models.Pagination;

    public class ProductsFlavoursDto
    {
        public ProductsFlavoursDto()
        {
            this.Flavours = new List<ProductFlavourDto>();
            this.PaginationData = new PaginationData();
        }

        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("data")]
        public IEnumerable<ProductFlavourDto> Flavours { get; set; }

        [JsonProperty("paginator")]
        public PaginationData PaginationData { get; set; }
    }
}
