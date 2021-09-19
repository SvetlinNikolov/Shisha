namespace ShishaProject.Services.Interfaces
{
    using ShishaProject.Services.Data.Models.Dtos;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<ProductsFlavoursDto> GetAllFlavours();
    }
}
