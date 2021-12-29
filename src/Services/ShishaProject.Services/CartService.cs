namespace ShishaProject.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Pagination;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Cart;

    public class CartService : ICartService
    {
        private readonly IUsersService usersService;
        private readonly IRestClient restClient;
        private readonly IOptions<CartEndpointsConfig> endpointConfig;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartService(
            IUsersService usersService,
            IRestClient restClient,
            IOptions<CartEndpointsConfig> endpointConfig,
            IHttpContextAccessor httpContextAccessor)
        {
            this.usersService = usersService;
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddToCartAsync(AddToCartInputModel request)
        {
            var loggedInUser = await this.usersService.GetLoggedInUserAsync();
            request.UserId = int.Parse(loggedInUser.UserId);

            var requestJson = JsonConvert.SerializeObject(request);

            var response = await this.restClient.PostAsync<ShishaResponseDto>(this.endpointConfig.Value.AddToCart, requestJson);

            return !string.IsNullOrEmpty(response.Errors);
        }

        public async Task Checkout()
        {
            var cartProducts = await this.GetCartAsync();

            if (!cartProducts.Flavours.Any())
            {

            }
        }

        public async Task<ProductsFlavoursDto> GetCartAsync()
        {
            var loggedInUser = await this.usersService.GetLoggedInUserAsync();

            var products = await this.restClient.PostAsync<ProductsFlavoursDto>(
                this.endpointConfig.Value.GetCart,
                JsonHelper.SerializeToPhpApiFormat("user_id", int.Parse(loggedInUser.UserId)));

            var pager = new Pager(products.Flavours.Count());
            products.PaginationData.Pages = pager.Pages;

            return products;
        }

        public void GetCartById(int cartId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(int flavourId)
        {
            throw new NotImplementedException();
        }
    }
}
