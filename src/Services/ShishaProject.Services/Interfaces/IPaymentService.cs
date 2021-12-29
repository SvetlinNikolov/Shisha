namespace ShishaProject.Services.Interfaces
{
    using ShishaProject.Services.Data.Interfaces;
    using System;

    public interface IPaymentService
    {
        void MakePayment<T>(T model)
            where T : IPaymentModel;

        bool AppliesTo(Type provider);
    }
}
