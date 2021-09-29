namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> LoginUserAsync(UserDto model);
        //IEnumerable<User> GetAll();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> RegisterUserAsync(RegistrationInputModel user);
        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }
}
