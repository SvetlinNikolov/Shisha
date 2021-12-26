namespace ShishaProject.Services.Interfaces
{
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.Cart;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task<bool> AddToCartAsync(AddToCartInputModel inputModel);

        void RemoveFromCart(int flavourId);

        void GetCartById(int cartId);

        Task<ProductsFlavoursDto> GetCart();
    }
}
