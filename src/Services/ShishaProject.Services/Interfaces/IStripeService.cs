namespace ShishaProject.Services.Interfaces
{
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Models.Payment;

    public interface IStripeService
    {
        StatusCodeResult CreatePayment(StripeChargeInputModel inputModel);
    }
}
