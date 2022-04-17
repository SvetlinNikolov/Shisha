namespace ShishaProject.Services.Data.Interfaces
{
    public interface IPaymentStrategy
    {
        void MakePayment<T>(T model)
            where T : IPaymentModel;
    }
}
