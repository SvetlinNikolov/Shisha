namespace ShishaProject.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels;
    using ShishaProject.Web.ViewModels.Shared;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICartService cartService;

        public HomeController(IProductsService productsService, ICartService cartService)
        {
            this.productsService = productsService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new LayoutViewModel()
            {
                ProductsInCartCount = 123,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
