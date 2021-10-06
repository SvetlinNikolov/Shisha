namespace ShishaProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Interfaces;

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
    }
}
