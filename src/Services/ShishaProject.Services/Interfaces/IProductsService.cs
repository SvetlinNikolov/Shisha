namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;

    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Filters;

    public interface IProductsService
    {
        Task<ProductsFlavoursDto> GetAllFlavours(GetAllFlavoursRequest request);

        Task<ProductsCategoriesDto> GetAllCategories();

        Task<ProductsFlavoursDto> GetFlavoursByCategoryId(FlavourByCategoryIdRequest request);

        Task<ProductFlavourDto> GetFlavourById(FlavourByIdRequest request);

        Task<ProductsFlavoursDto> GetFilteredFlavours(Filters filters);

        // Task<ProductsFlavoursDto> GetFlavoursByIds(IEnumerable<string> flavourIds);
    }
}
