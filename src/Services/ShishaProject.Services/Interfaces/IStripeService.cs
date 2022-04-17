namespace ShishaProject.Services.Interfaces
{
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Models.Payment;
    using Stripe;

    public interface IStripeService
    {
        PaymentIntent CreatePaymentIntent(StripeChargeInputModel inputModel);
    }
}
