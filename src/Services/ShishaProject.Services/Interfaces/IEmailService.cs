namespace ShishaProject.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        bool IsValidEmail(string email);
    }
}
