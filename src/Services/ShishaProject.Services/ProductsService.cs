namespace ShishaProject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Data.Models.Dtos.Api;
    using ShishaProject.Services.Data.Models.Filters;
    using ShishaProject.Services.Data.Models.Pagination;
    using ShishaProject.Services.Interfaces;

    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;
        private readonly IMapper mapper;
        private readonly IOptions<ProductsEndpointsConfig> endpointConfig;

        public ProductsService(
            IRestClient restClient,
            IMapper mapper,
            IOptions<ProductsEndpointsConfig> endpointConfig)
        {
            this.restClient = restClient;
            this.mapper = mapper;
            this.endpointConfig = endpointConfig;
        }

        public async Task<ProductsFlavoursDto> GetAllFlavours(string language)
        {
            var allProducts = await this.restClient.GetAsync<ProductsFlavoursDto>(this.endpointConfig.Value.GetAllFlavours, language);

            var pager = new Pager(allProducts.Flavours.Count());
            allProducts.PaginationData.Pages = pager.Pages;

            return allProducts;
        }

        public async Task<ProductsCategoriesDto> GetAllCategories()
        {
            return await this.restClient.GetAsync<ProductsCategoriesDto>(this.endpointConfig.Value.GetAllCategories);
        }

        public async Task<ProductsFlavoursDto> GetFlavoursByCategoryId(FlavourByCategoryIdRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);

            ProductsFlavoursDto dto = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetFlavourByCategoryId,
                    requestJson);

            return dto;
        }

        public async Task<ProductFlavourDto> GetFlavourById(FlavourByIdRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var result = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetFlavourById,
                    requestJson);

            var dto = result.Flavours?.FirstOrDefault();

            return dto;
        }

        public async Task<ProductsFlavoursDto> GetFilteredFlavours(Filters filters = null)
        {
            var filtersJson = JsonConvert.SerializeObject(filters);

            var dto = await this.restClient
                 .PostAsync<ProductsFlavoursDto>(
                     this.endpointConfig.Value.Filters,
                     filtersJson);

            var pager = new Pager(dto.Flavours.Count());
            dto.PaginationData.Pages = pager.Pages;

            return dto;
        }
    }
}
