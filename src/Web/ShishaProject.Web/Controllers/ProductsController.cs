namespace ShishaProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Filters;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Common.ExtensionMethods;
    using System.IO;
    using System.Text;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.productsService.GetAllFlavours(this.Language);
            return this.View(products);
        }

        public async Task<IActionResult> FlavourDetails(int id)
        {
            var product = await this.productsService.GetFlavourById(new FlavourByIdRequest { FlavourId = id, Language = this.Language });

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredFlavours([FromBody] Root root)
        {
            return this.Json("az sym qk");
        }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Filters
    {
        public string price_from { get; set; }
        public string price_to { get; set; }
        public List<object> category_id { get; set; }
        public List<object> svetlio { get; set; }
        public List<object> svetlio2 { get; set; }
        public bool in_stock { get; set; }
    }

    public class Data
    {
        public string currentPageNumber { get; set; }
        public string selectValue { get; set; }
        public Filters filters { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }


}
