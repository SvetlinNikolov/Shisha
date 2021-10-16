namespace ShishaProject.Services.Data.Models.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ShishaProject.Services.Data.Models.Dtos;

    public class FiltersResults
    {
        IEnumerable<ProductFlavourDto> FilteredFlavours { get; set; }
    }
}
