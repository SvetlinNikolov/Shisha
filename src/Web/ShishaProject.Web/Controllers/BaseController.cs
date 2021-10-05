namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected string Language
        {
            get { return (string)this.ControllerContext.RouteData.Values["language"]; }
        }
    }
}
