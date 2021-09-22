﻿namespace ShishaProject.Services.Data.Models.Dtos
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ProductsFlavoursDto
    {
        [JsonProperty("flavours")]
        public IEnumerable<ProductFlavourDto> Flavours { get; set; }
    }
}