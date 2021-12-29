namespace ShishaProject.Services
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Interfaces;
    using ShishaProject.Services.Interfaces;

    public abstract class PaymentService<TModel> : IPaymentService
    where TModel : IPaymentModel
    {
        public virtual bool AppliesTo(Type provider)
        {
            return typeof(TModel).Equals(provider);
        }

        public void MakePayment<T>(T model)
            where T : IPaymentModel
        {
            this.MakePayment((TModel)(object)model);
        }

        protected abstract StatusCodeResult MakePayment(TModel model);
    }
}
