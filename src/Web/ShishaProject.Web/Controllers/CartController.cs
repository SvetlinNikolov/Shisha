﻿namespace ShishaProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
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
            var count = await this.cartService.GetProductsInCartCount();

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
