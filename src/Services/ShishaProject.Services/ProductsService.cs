namespace ShishaProject.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;

    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;
        private readonly IOptions<EndpointConfig> endpointConfig;

        public ProductsService(
            IRestClient restClient,
            IOptions<EndpointConfig> endpointConfig)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
        }

        public async Task<ProductsFlavoursDto> GetAllFlavours()
            => await this.restClient.GetAsync<ProductsFlavoursDto>(this.endpointConfig.Value.GetAllFlavours);

        public async Task<ProductsCategoriesDto> GetAllCategories()
            => await this.restClient.GetAsync<ProductsCategoriesDto>(this.endpointConfig.Value.GetAllCategories);
    }
}
