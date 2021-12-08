namespace ShishaProject.Services.Interfaces
{
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Web.ViewModels.Payment;

    public interface IStripeService
    {
        StatusCodeResult CreatePayment(StripeChargeInputModel inputModel);
    }
}
