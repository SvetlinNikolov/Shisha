namespace ShishaProject.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Web.ViewModels.User;

    public interface IUsersService
    {
        Task<bool> AuthenticateUserAsync(LoginInputModel model);

        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto> GetUserByUsernameOrEmailAsync(string usernameOrEmail);

        Task<UserDto> GetLoggedInUserAsync();

        Task<UserDto> RegisterUserAsync(RegistrationInputModel user);

        bool UserLoggedIn();

        Task LoginUserAsync(LoginInputModel inputModel);

        Task LogoutUserAsync();

        Task ResetUserPasswordAsync(string passwordToken);

        Task<bool> UpdateUserConfirmedEmailAsync(string confirmEmailToken);

        Task UpdateUserAsync(UserDto userModel);

        Task ConfirmUserEmail(ConfirmUserEmailDto userModel);

        //void Delete(int id);
    }
}
