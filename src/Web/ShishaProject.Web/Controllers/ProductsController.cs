namespace ShishaProject.Web.Controllers
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
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
            var products = await this.productsService.GetFlavoursByCategoryId(1);
            return this.View();
        }
    }
}
