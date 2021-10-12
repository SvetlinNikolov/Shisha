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
    using Newtonsoft.Json.Linq;

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
        public async Task<IActionResult> GetFilteredFlavours([FromBody] Data data)
        {
            var filteredFlavours = await this.productsService.GetFilteredFlavours(JsonConvert.SerializeObject(data));
            return this.Json("az pak sym qk");
        }
    }
}
