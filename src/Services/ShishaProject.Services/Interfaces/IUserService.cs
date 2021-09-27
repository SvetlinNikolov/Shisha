using System.Threading.Tasks;

namespace ShishaProject.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(dynamic user);
    }
}
