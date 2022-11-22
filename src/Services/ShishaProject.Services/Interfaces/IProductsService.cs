namespace ShishaProject.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ShishaProject.Common;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Filters;

    public interface IProductsService
    {
        Task<ProductsFlavoursDto> GetAllFlavours(GetAllFlavoursRequest request);

        Task<ProductsCategoriesDto> GetAllCategories();

        Task<ProductsFlavoursDto> GetFlavoursByCategoryId(FlavourByCategoryIdRequest request);

        Task<ProductFlavourDto> GetFlavourById(FlavourByIdRequest request, bool includeRelatedFlavours = false);

        Task<ProductsFlavoursDto> GetFilteredFlavours(Filters filters);

        public Task<IEnumerable<ProductFlavourDto>> GetRelatedFlavours(RelatedFlavoursRequest request, int take = GlobalConstants.RelatedFlavoursCount);

        Task<ProductsFlavoursDto> SearchAsync(SearchFlavourRequest request);
    }
}
