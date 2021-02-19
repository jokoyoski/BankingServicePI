using Common;
using System.Threading.Tasks;

namespace BankingAPI.PaymentGateWay.Contracts
{
    public interface ICheapPaymentGateway
    {
        Task<string> ProcessPayment(ProcessPayment processPayment );
    }
}
