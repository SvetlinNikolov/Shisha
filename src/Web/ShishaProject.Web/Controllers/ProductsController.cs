namespace ShishaProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Localization;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Filters;
    using ShishaProject.Services.Interfaces;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IHtmlLocalizer<ProductsController> localizer;

        public ProductsController(IProductsService productsService, IHtmlLocalizer<ProductsController> localizer)
        {
            this.productsService = productsService;
            this.localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.productsService.GetAllFlavours(new GetAllFlavoursRequest { Language = this.Language });
            return this.View(products);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredFlavours(/*[FromBody]*/ Filters filters)
        {
            var filteredFlavours = await this.productsService.GetFilteredFlavours(filters);
            return this.PartialView("_Flavours", filteredFlavours);
        }

        public async Task<IActionResult> FlavourDetails(int id)
        {
            var product = await this.productsService.GetFlavourById(new FlavourByIdRequest { FlavourId = id, Language = this.Language });

            return this.View(product);
        }
    }
}
