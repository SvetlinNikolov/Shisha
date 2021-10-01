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
            return this.RedirectToAction("LoginUser", "Users");
            var products = await this.userService.GetUserByIdAsync(1);
            return this.View();
        }

        public IActionResult Index2()
        {
            return this.Json("lg g2 > sgs4");
        }
    }
}
