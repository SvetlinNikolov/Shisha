namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;
    using System.Threading.Tasks;

    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult AddToCart(AddToCartInputModel inputModel)
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            await this.cartService.GetCart();
            return this.View();
        }
    }
}
