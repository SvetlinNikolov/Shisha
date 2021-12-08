namespace ShishaProject.Services
{
    using System.Net.Mail;

    using ShishaProject.Services.Interfaces;

    public class EmailService : IEmailService
    {
        public bool IsValidEmail(string email)
            => MailAddress.TryCreate(email, out _);
    }
}
