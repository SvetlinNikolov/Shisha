namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.User;

    public interface IUsersService
    {
        Task<bool> AuthenticateUser(LoginInputModel model);

        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto> GetUserByUsernameOrEmailAsync(string usernameOrEmail);

        Task<bool> RegisterUserAsync(RegistrationInputModel user);

        bool UserLoggedIn();

        Task LoginUser(LoginInputModel inputModel);

        Task LogoutUser();

        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }
}
