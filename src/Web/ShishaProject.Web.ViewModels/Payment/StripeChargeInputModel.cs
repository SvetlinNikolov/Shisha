namespace ShishaProject.Web.ViewModels.Payment
{
    public class StripeChargeInputModel
    {
        public string ProductName { get; set; }

        public long Price { get; set; }

        public int Quantity { get; set; }
    }
}
