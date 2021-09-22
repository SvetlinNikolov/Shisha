namespace ShishaProject.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
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
        {
            return await this.restClient.GetAsync<ProductsFlavoursDto>(this.endpointConfig.Value.GetAllFlavours);
        }

        public async Task<ProductsCategoriesDto> GetAllCategories()
        {
            return await this.restClient.GetAsync<ProductsCategoriesDto>(this.endpointConfig.Value.GetAllCategories);
        }

        public async Task<ProductsFlavoursDto> GetFlavoursByCategoryId(int categoryId)
        {
            var json = JsonConvert.SerializeObject(new GetProductFlavourByCategoryIdRequest { CategoryId = categoryId });

            ProductsFlavoursDto dto = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetFlavourByCategoryId,
                    json);
            //todo make Service that handles status codes
            return dto;
        }

        public async Task<ProductFlavourDto> GetFlavourById(int flavourId)
        {
            var json = JsonConvert.SerializeObject(new GetProductFlavourRequest { FlavourId = flavourId });

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