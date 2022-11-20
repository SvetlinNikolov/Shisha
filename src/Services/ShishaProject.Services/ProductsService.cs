namespace ShishaProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShishaProject.Common;
    using ShishaProject.Common.ExceptionHandling;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Services.Data.Enums;
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
        private readonly IShishaLogger logger;

        public ProductsService(
            IRestClient restClient,
            IMapper mapper,
            IOptions<ProductsEndpointsConfig> endpointConfig,
            IShishaLogger logger)
        {
            this.restClient = restClient;
            this.mapper = mapper;
            this.endpointConfig = endpointConfig;
            this.logger = logger;
        }

        public async Task<ProductsFlavoursDto> GetAllFlavours(GetAllFlavoursRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var allFlavours =
                await this.restClient.PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetAllFlavours,
                    requestJson);

            var pager = new Pager(allFlavours.PaginationData.TotalProducts, allFlavours.PaginationData.CurrentPage,
                allFlavours.PaginationData.ItemsPerPage);
            allFlavours.PaginationData.Pages = pager.Pages;

            return allFlavours;
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

        public async Task<ProductFlavourDto> GetFlavourById(
            FlavourByIdRequest request,
            bool includeRelatedFlavours = false)
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var result = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.GetFlavourById,
                    requestJson);

            var dto = result.Flavours?.FirstOrDefault();

            if (includeRelatedFlavours && dto?.FlavourType != null && dto.FlavourType != FlavourType.Unspecified)
            {
                dto.RelatedFlavours = await this.GetRelatedFlavours(
                    new RelatedFlavoursRequest
                    {
                        FlavourType = dto.FlavourType,
                        Language = request.Language,
                        FlavourId = request.FlavourId,
                    });
            }

            return dto;
        }

        public async Task<ProductsFlavoursDto> GetFilteredFlavours(Filters filters = null)
        {
            var filtersJson = JsonConvert.SerializeObject(filters);

            var dto = await this.restClient
                .PostAsync<ProductsFlavoursDto>(
                    this.endpointConfig.Value.Filters,
                    filtersJson);

            // burov api returns last filter not current filters
            if (dto.Flavours.IsNullOrEmpty())
            {
                return dto;
            }

            var pager = new Pager(dto.PaginationData.TotalProducts,
                                  dto.PaginationData.CurrentPage,
                                  itemsPerPage: dto.PaginationData.ItemsPerPage);

            dto.PaginationData.Pages = pager.Pages;

            return dto;
        }

        public async Task<IEnumerable<ProductFlavourDto>> GetRelatedFlavours(
            RelatedFlavoursRequest request,
            int take = GlobalConstants.RelatedFlavoursCount)
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var relatedFlavours = Enumerable.Empty<ProductFlavourDto>();
            try
            {
                var dto = await this.restClient
                    .PostAsync<ProductsFlavoursDto>(
                        this.endpointConfig.Value.GetRelatedFlavours,
                        requestJson);

                if (!dto.Flavours.IsNullOrEmpty())
                {
                    relatedFlavours = dto.Flavours;
                }
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return relatedFlavours;
        }
    }
}
