namespace ShishaProject.Services.Interfaces
{
    using ShishaProject.Web.ViewModels.Cart;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task<bool> AddToCart(AddToCartInputModel inputModel);

        void RemoveFromCart(int flavourId);

        void GetCartById(int cartId);

        Task GetCart();
    }
}
