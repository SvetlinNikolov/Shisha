﻿namespace ShishaProject.Services.Data.Models.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Newtonsoft.Json;

    public class ProductsCategoriesDto
    {
        [JsonProperty("categories")]
        public IEnumerable<ProductCategoryDto> Categories { get; set; }
    }
}