namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
