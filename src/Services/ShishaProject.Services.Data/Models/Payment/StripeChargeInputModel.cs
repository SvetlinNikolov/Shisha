namespace ShishaProject.Services.Data.Models.Payment
{

    using ShishaProject.Services.Data.Interfaces;

    public class StripeChargeInputModel : IPaymentModel
    {
        public string ProductName { get; set; }

        public long Price { get; set; }

        public int Quantity { get; set; }
    }
}
