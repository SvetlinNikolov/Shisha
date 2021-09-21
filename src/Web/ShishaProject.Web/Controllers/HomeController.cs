namespace ShishaProject.Web.Controllers
{
    using System.Diagnostics;

    using ShishaProject.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Interfaces;
    using System.Threading.Tasks;
    using System.Net.NetworkInformation;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var prods = await this.productsService.GetAllCategories();
            return this.View();
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
