namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Web.ViewModels.Cart;

    public interface ICartService
    {
        Task<bool> AddToCartAsync(AddToCartInputModel inputModel);

        Task RemoveFromCartAsync(RemoveFromCartRequest inputModel);

        void GetCartById(int cartId);

        Task<ProductsFlavoursDto> GetCartAsync();

        Task<JsonResult> Checkout();
    }
}
