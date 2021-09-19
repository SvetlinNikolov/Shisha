namespace ShishaProject.Services
{
    using System.Threading.Tasks;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;

    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;

        public ProductsService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ProductsFlavoursDto> GetAllFlavours()
            => await this.restClient.GetAsync<ProductsFlavoursDto>("get-all-flavours");
    }
}
