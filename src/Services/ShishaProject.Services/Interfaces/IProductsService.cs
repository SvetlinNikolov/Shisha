namespace ShishaProject.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ShishaProject.Services.Data.Models.Dtos;

    public interface IProductsService
    {
        Task<ProductsFlavoursDto> GetAllFlavours();

        Task<ProductsCategoriesDto> GetAllCategories();

        Task<ProductsFlavoursDto> GetFlavoursByCategoryId(int categoryId);

        Task<ProductFlavourDto> GetFlavourById(int flavourId);

        // Task<ProductsFlavoursDto> GetFlavoursByIds(IEnumerable<string> flavourIds);
    }
}
