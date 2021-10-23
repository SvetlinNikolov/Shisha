namespace ShishaProject.Services.Data.Models.Authentication
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class CookieAuthenticationEvent : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> redirectContext)
        {
            var context = redirectContext.HttpContext;
  
            redirectContext.RedirectUri = "Your new url";
            return base.RedirectToLogin(redirectContext);
        }
    }
}
