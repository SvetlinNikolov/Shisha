﻿namespace ShishaProject.Services.Data.Models.Configs
{
    public class ProductsEndpointsConfig
    {
        public string GetAllFlavours { get; set; }

        public string GetFlavourByCategoryId { get; set; }

        public string GetFlavourById { get; set; }

        public string GetFlavoursByIds { get; set; }

        public string GetAllCategories { get; set; }
    }
}