namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;

    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.Cart;

    public interface ICartService
    {
        Task<bool> AddToCartAsync(AddToCartInputModel inputModel);

        void RemoveFromCart(int flavourId);

        void GetCartById(int cartId);

        Task<ProductsFlavoursDto> GetCartAsync();
    }
}
