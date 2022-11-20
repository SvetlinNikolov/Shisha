namespace ShishaProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using ShishaProject.Common.Caching;
    using ShishaProject.Common.Constants;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IShishaCache shishaCache;
        private readonly IUsersService usersService;

        public CartController(ICartService cartService, IShishaCache shishaCache, IUsersService usersService)
        {
            this.cartService = cartService;
            this.shishaCache = shishaCache;
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
            // TODO JS THAT CALLS THIS METHOD
            var count = this.shishaCache.Get<int>(CacheConstants.PRODUCTS_IN_CART_COUNT_CACHE_KEY);

            if (count.Equals(CacheConstants.PRODUCTS_IN_CART_INVALID_CACHE_COUNT))
            {
                count = await this.cartService.GetProductsInCartCountAsync(); //could have problem with async caching
                this.shishaCache.SetOrUpdate(CacheConstants.PRODUCTS_IN_CART_COUNT_CACHE_KEY, count);
                this.ViewData["ItemsInCart"] = count;
            }

            return count;
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

            await this.cartService.AddToCartAsync(inputModel);

            return this.Json("THIS IS RETURNED AFTER SUCCESSFULL ADD");
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
