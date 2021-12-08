using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShishaProject.Services.Interfaces;
using ShishaProject.Web.ViewModels.Payment;
using Stripe;
using Stripe.Checkout;

namespace ShishaProject.Web.Controllers
{
    public class StripeController : BaseController
    {
        private readonly IStripeService paymentService;

        public StripeController(IStripeService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Checkout(StripeChargeInputModel inputModel)
        {
            this.paymentService.CreatePayment(new StripeChargeInputModel { Price = 100, ProductName = "Test", Quantity = 1 });
            throw new System.Exception("yes sir payment");
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            return this.View();
        }
    }
}
