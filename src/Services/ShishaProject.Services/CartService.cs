namespace ShishaProject.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
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

            var allProducts = await this.restClient.PostAsync<dynamic>(this.endpointConfig.Value.AddToCart, requestJson);
            throw new NotImplementedException();
        }

        public async Task<ProductsFlavoursDto> GetCart()
        {
            var loggedInUser = await this.usersService.GetLoggedInUserAsync();

            return await this.restClient.PostAsync<ProductsFlavoursDto>(
                this.endpointConfig.Value.GetCart,
                JsonHelper.SerializeToPhpApiFormat("user_id", int.Parse(loggedInUser.UserId)));
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
