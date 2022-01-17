namespace ShishaProject.Web.Controllers
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class BaseController : Controller
    {
        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            this.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });

            return this.LocalRedirect(returnUrl);
        }

        protected string GetLanguage()
         {
            return this.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName] ??
                   (string)this.ControllerContext.RouteData.Values["language"];
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.ViewData["Language"] = this.GetLanguage();

            base.OnActionExecuted(filterContext);
        }

        protected string RemoveController(string value)
        {
            string result = value.Replace("Controller", string.Empty);

            return result;
        }
    }
}
