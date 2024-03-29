﻿namespace ShishaProject.Services
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShishaProject.Common.Caching;
    using ShishaProject.Common.Constants;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Pagination;
    using ShishaProject.Services.Data.Models.Payment;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    public class CartService : ICartService
    {
        private readonly IUsersService usersService;
        private readonly IRestClient restClient;
        private readonly IOptions<CartEndpointsConfig> endpointConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IStripeService stripeService;
        private readonly IShishaCache shishaCache;

        public CartService(
            IUsersService usersService,
            IRestClient restClient,
            IOptions<CartEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContextAccessor,
            IStripeService stripeService,
            IShishaCache shishaCache)
        {
            this.usersService = usersService;
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContextAccessor;
            this.stripeService = stripeService;
            this.shishaCache = shishaCache;
        }

        public async Task<bool> AddToCartAsync(AddToCartInputModel request)
        {
            var loggedInUser = await this.usersService.GetLoggedInUserAsync();
            request.UserId = int.Parse(loggedInUser.UserId);

            var requestJson = JsonConvert.SerializeObject(request);

            var response = await this.restClient.PostAsync<ShishaResponseDto>(this.endpointConfig.Value.AddToCart, requestJson);

            var isAdded = response.Errors.IsNullOrEmpty() == true;

            if (isAdded)
            {
                this.shishaCache.SetOrUpdate(CacheConstants.PRODUCTS_IN_CART_COUNT_CACHE_KEY, CacheConstants.PRODUCTS_IN_CART_INVALID_CACHE_COUNT);
            }

            return isAdded;
        }

        public async Task<JsonResult> Checkout()
        {
            var cartProducts = await this.GetCartAsync();

            if (cartProducts.Flavours.Any())
            {
                long price = this.CalculatePrice(cartProducts);

                var intent = this.stripeService.CreatePaymentIntent(new StripeChargeInputModel { Price = price });

                return new JsonResult(new { clientSecret = intent.ClientSecret });
            }

            return new JsonResult(HttpStatusCode.BadRequest);
        }

        public async Task<ProductsFlavoursDto> GetCartAsync()
        {
            var loggedInUser = await this.usersService.GetLoggedInUserAsync();

            var products = await this.restClient.PostAsync<ProductsFlavoursDto>(
                this.endpointConfig.Value.GetCart,
                JsonHelper.SerializeToPhpApiFormat("user_id", int.Parse(loggedInUser.UserId)));

            var pager = new Pager(products.Flavours.EmptyIfNull().Count());
            products.PaginationData.Pages = pager.Pages;

            return products;
        }

        public void GetCartById(int cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetProductsInCartCountAsync()
        {
            var userId = await this.usersService.GetLoggedInUserIdAsync();

            var productsInCartCount = await this.restClient.PostAsync<ShishaResponseDto<int>>(
                          this.endpointConfig.Value.GetProductsInCartCount,
                          JsonHelper.SerializeToPhpApiFormat("user_id", userId));

            return productsInCartCount.Data;
        }

        public async Task<bool> RemoveFromCartAsync(RemoveFromCartRequest inputModel)
        {
            inputModel.UserId = await this.usersService.GetLoggedInUserIdAsync();
            inputModel.Quantity = inputModel.Quantity == 0 ? int.MaxValue : inputModel.Quantity;

            var request = JsonConvert.SerializeObject(inputModel);

            var response = await this.restClient.PostAsync<ShishaResponseDto>(
                 this.endpointConfig.Value.RemoveFromCart,
                 request);

            var isRemoved = response.Errors.IsNullOrEmpty() == true;

            if (isRemoved)
            {
                this.shishaCache.SetOrUpdate(CacheConstants.PRODUCTS_IN_CART_COUNT_CACHE_KEY, CacheConstants.PRODUCTS_IN_CART_INVALID_CACHE_COUNT);
            }

            return isRemoved;
        }

        private long CalculatePrice(ProductsFlavoursDto cartProducts)
        {
            return (long)cartProducts.Flavours.Select(x => x.Price).Sum() * 100;
        }
    }
}
