namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class BaseController : Controller
    {
        protected string Language
        {
            get { return (string)this.ControllerContext.RouteData.Values["language"]; }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.ViewData["Language"] = this.Language;

            base.OnActionExecuted(filterContext);
        }
    }
}
