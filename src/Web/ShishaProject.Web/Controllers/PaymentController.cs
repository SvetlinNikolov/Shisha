using Microsoft.AspNetCore.Mvc;
using ShishaProject.Services;
using ShishaProject.Services.Data.Interfaces;
using ShishaProject.Services.Data.Models.Payment;
using ShishaProject.Services.Interfaces;
using ShishaProject.Services.Strategy;
using System.Threading.Tasks;

namespace ShishaProject.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentStrategy paymentStrategy;

        public PaymentController(IPaymentStrategy paymentStrategy)
        {
            this.paymentStrategy = paymentStrategy;
        }

        public async Task<IActionResult> Checkout()
        {
            this.paymentStrategy.MakePayment(new StripeChargeInputModel());


            this.paymentStrategy.MakePayment(new StripeChargeInputModel());
            throw new System.Exception("pesho");
        }
    }
}
