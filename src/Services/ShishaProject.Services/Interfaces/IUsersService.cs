namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> AuthenticateUser(LoginInputModel model);
        //IEnumerable<User> GetAll();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> RegisterUserAsync(RegistrationInputModel user);

        bool UserLoggedIn();
        Task LoginUser(LoginInputModel inputModel);
        Task LogoutUser();

        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }
}
