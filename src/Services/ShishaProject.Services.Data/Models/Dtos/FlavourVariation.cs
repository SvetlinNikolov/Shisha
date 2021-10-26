using System;
using System.Collections.Generic;
using System.Text;

namespace ShishaProject.Services.Data.Models.Dtos
{
    public class FlavourVariation
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string weight { get; set; }
        public int price { get; set; }
        public int flavour_id { get; set; }
    }
}
