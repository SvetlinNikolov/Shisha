namespace ShishaProject.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        bool IsValidEmail(string email);

        Task SendConfirmEmailMessageAsync(string to, string subject, string message, string confirmEmailLink);

        Task SendUserResetPasswordEmailAsync(string to, string subject, string message, string confirmPasswordLink);
    }
}
