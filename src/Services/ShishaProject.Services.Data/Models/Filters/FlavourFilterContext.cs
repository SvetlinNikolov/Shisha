namespace ShishaProject.Services.Data.Models.Filters
{
    public class FlavourFilterContext
    {
        public string PriceFrom { get; set; }

        public string PriceTo { get; set; }

        public bool InStock { get; set; }

        public int CategoryId { get; set; }
    }
}
