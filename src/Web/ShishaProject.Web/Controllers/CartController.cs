namespace ShishaProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartProducts = await this.cartService.GetCartAsync();

            if (!cartProducts.Flavours.Any())
            {
                return this.View("_CartEmpty");
            }

            return this.View(cartProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartInputModel inputModel)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(nameof(UsersController.LoginUser), this.RemoveController(nameof(UsersController)));
            }

            if (!this.ModelState.IsValid)
            {
                return this.Json("Something went wrong"); //TODO translation error goes here<<<
            }

            var isAdded = await this.cartService.AddToCartAsync(inputModel);

            if (isAdded)
            {
                //this.UpdateCountOfProductsInCart
            }

            return this.Json("Something went wrong");
        }

        [Route("create-payment-intent")]
        public async Task<IActionResult> Checkout()
        {
            var result = await this.cartService.Checkout();

            return result;
        }
    }
}
