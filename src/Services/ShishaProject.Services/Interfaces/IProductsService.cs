namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;

    using ShishaProject.Services.Data.Models.Dtos;

    public interface IProductsService
    {
        Task<ProductsFlavoursDto> GetAllFlavours();

        Task<ProductsCategoriesDto> GetAllCategories();
    }
}
