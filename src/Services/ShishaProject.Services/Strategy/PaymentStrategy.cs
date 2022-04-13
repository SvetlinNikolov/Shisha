namespace ShishaProject.Services.Strategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ShishaProject.Services.Data.Interfaces;
    using ShishaProject.Services.Interfaces;

    public class PaymentStrategy : IPaymentStrategy
    {
        private readonly IEnumerable<IPaymentService> paymentServices;

        public PaymentStrategy(IEnumerable<IPaymentService> paymentServices)
        {
            this.paymentServices = paymentServices ?? throw new ArgumentNullException(nameof(paymentServices));
        }

        public void MakePayment<T>(T model)
            where T : IPaymentModel
        {
            this.GetPaymentService(model).MakePayment(model);
        }

        private IPaymentService GetPaymentService<T>(T model)
            where T : IPaymentModel
        {
            var result = this.paymentServices.FirstOrDefault(p => p.AppliesTo(model.GetType()));
            if (result == null)
            {
                throw new InvalidOperationException(
                    $"Payment service for {model.GetType()} not registered.");
            }

            return result;
        }
    }
}
