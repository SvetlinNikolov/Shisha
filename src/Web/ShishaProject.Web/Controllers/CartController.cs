namespace ShishaProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IMemoryCache memoryCache;
        private readonly IUsersService usersService;
        private const string PRODUCTS_IN_CART_COUNT_CACHE_KEY = $"Products_In_Cart_Count_";

        public CartController(ICartService cartService, IMemoryCache memoryCache, IUsersService usersService)
        {
            this.cartService = cartService;
            this.memoryCache = memoryCache;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var cartProducts = await this.cartService.GetCartAsync();

            if (!cartProducts.Flavours.EmptyIfNull().Any())
            {
                return this.View("_CartEmpty");
            }

            return this.View(cartProducts);
        }

        public async Task<int> GetProductsInCartCount()
        {
            var count = this.memoryCache.Get<int?>(nameof(this.GetProductsInCartCount));this.usersService.GetLoggedInUserAsync();

            if (count != null)
            {
                count = await this.cartService.GetProductsInCartCountAsync();
                this.memoryCache.Set(PRODUCTS_IN_CART_COUNT_CACHE_KEY + this.usersService, count);
            }

            return count.GetValueOrDefault();
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

        public async Task RemoveFromCart([FromBody] RemoveFromCartRequest inputModel)
        {
            await this.cartService.RemoveFromCartAsync(inputModel);
        }

        [Route("create-payment-intent")]
        public async Task<IActionResult> Checkout()
        {
            var result = await this.cartService.Checkout();

            return result;
        }
    }
}
