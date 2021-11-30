namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Web.ViewModels.Cart;

    public class CartController : Controller
    {
        public IActionResult AddToCart(AddToCartInputModel inputModel)
        {
            return this.View();
        }
    }
}
