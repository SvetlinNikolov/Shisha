namespace ShishaProject.Services
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;

    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;
        private readonly IOptions<ProductsEndpointsConfig> endpointConfig;

        public ProductsService(
            IRestClient restClient,
            IOptions<ProductsEndpointsConfig> endpointConfig)
        {
            this.restClient = restClient;
            this.endpointConfig = endpointConfig;
        }

        public async Task<ProductsFlavoursDto> GetAllFlavours()
        {
            return await this.restClient.GetAsync<ProductsFlavoursDto>(this.endpointConfig.Value.GetAllFlavours);
        }

        public async Task<ProductsCategoriesDto> GetAllCategories()
        {
            return await this.restClient.GetAsync<ProductsCategoriesDto>(this.endpointConfig.Value.GetAllCategories);
        }

        public async Task<ProductsFlavoursDto> GetFlavoursByCategoryId(int categoryId)
        {
            var json = JsonHelper.SerializeToPhpApiFormat("category_id", categoryId);

            ProductsFlavoursDto dto = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetFlavourByCategoryId,
                    json);

            return dto;
        }

        public async Task<ProductFlavourDto> GetFlavourById(int flavourId)
        {
            var json = JsonHelper.SerializeToPhpApiFormat("id", flavourId);

            JObject result = await this.restClient
                .PostAsync<JObject>(
                    this.endpointConfig.Value.GetFlavourById,
                    json);

            ProductFlavourDto dto = result.Value<JObject>("flavour")
                .ToObject(typeof(ProductFlavourDto)) as ProductFlavourDto;

            return dto;
        }
    }
}
