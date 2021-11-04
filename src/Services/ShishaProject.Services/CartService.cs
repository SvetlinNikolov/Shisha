namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Options;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Interfaces;

    public class CartService : ICartService
    {
        private readonly IUsersService usersService;
        private readonly IOptions<CartEndpointsConfig> endpointConfig;

        public CartService(
            IUsersService usersService,
            IOptions<CartEndpointsConfig> endpointConfig)
        {
            this.usersService = usersService;
            this.endpointConfig = endpointConfig;
        }

        public void AddToCart(int flavourId)
        {
            throw new NotImplementedException();
        }

        public void GetCart()
        {
            throw new NotImplementedException();
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
