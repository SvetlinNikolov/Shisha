namespace ShishaProject.Services.Data.Models.Payment
{

    using ShishaProject.Services.Data.Interfaces;

    public class StripeChargeInputModel : IPaymentModel
    {
        public long Price { get; set; }
    }
}
