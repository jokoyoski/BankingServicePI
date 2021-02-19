using BankingAPI.Repository.Model;

namespace BankingAPI.Interface.contracts
{
    public interface IPaymentRepository
   {
        int ProcessPayment(Payment payment);

   }
}
