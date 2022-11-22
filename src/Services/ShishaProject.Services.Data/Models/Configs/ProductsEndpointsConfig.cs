namespace ShishaProject.Services.Data.Models.Configs
{
    public class ProductsEndpointsConfig
    {
        public string GetAllFlavours { get; set; }

        public string GetFlavourByCategoryId { get; set; }

        public string GetFlavourById { get; set; }

        public string GetFlavoursByIds { get; set; }

        public string GetAllCategories { get; set; }

        public string Filters { get; set; }

        public string GetRelatedFlavours { get; set; }

        public string Search { get; set; }
    }
}
