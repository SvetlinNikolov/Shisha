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
        private readonly IUsersService userService;

        public ProductsController(
            IProductsService productsService,
            IUsersService userService)
        {
            this.productsService = productsService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.productsService.GetAllFlavours();

            return this.View(products);
        }

        public IActionResult Index2()
        {
            return this.Json("lg g2 > sgs4");
        }
    }
}
