namespace ShishaProject.Services.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int flavourId);

        void RemoveFromCart(int flavourId);

        void GetCartById(int cartId);

        void GetCart();
    }
}
