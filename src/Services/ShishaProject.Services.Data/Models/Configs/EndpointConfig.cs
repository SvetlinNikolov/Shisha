﻿namespace ShishaProject.Services.Data.Models.Configs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EndpointConfig
    {
        public string BaseUri { get; set; }

        public string GetAllFlavours { get; set; }

        public string GetFlavourByCategoryId { get; set; }

        public string GetFlavourById { get; set; }

        public string GetFlavoursByIds { get; set; }

        public string GetAllCategories { get; set; }
    }
}
