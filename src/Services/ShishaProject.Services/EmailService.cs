namespace ShishaProject.Services
{
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Services.Messaging;

    public class EmailService : IEmailService
    {
        private readonly IEmailSender emailSender;

        public EmailService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task SendConfirmEmailMessageAsync(string to, string subject, string message, string confirmEmailLink)
        {
            var sb = new StringBuilder();

            sb.AppendLine(message)
                    .AppendLine(confirmEmailLink);

            await this.emailSender.SendEmailAsync(to, subject, sb.ToString());
        }

        public async Task ReceiveConfirmEmailMessage()
        {

        }

        public bool IsValidEmail(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }
    }
}
