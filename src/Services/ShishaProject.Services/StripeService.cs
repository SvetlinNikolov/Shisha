namespace ShishaProject.Services
{
    using ShishaProject.Services.Data.Interfaces;
    using ShishaProject.Services.Data.Models.Payment;
    using ShishaProject.Services.Interfaces;
    using Stripe;
    using System;

    public class StripeService : IStripeService
    {
        public PaymentIntent CreatePaymentIntent(StripeChargeInputModel inputModel)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = inputModel.Price,
                Currency = "bgn",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return paymentIntent;
        }
    }
}
