namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ShishaProject.Services.Interfaces;

    public class CartService : ICartService
    {
        private readonly IUsersService usersService;

        public CartService(IUsersService usersService)
        {
            this.usersService = usersService;
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
