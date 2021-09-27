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
        private readonly IUserService userService;

        public ProductsController(IProductsService productsService,
            IUserService userService)
        {
            this.productsService = productsService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            this.userService.RegisterUser(null);
            var products = await this.productsService.GetFlavoursByCategoryId(176);
            return this.View();
        }
    }
}
