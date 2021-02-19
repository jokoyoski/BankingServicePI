using BankingAPI.Service.RequestModel;
using Common.Model;
using System.Threading.Tasks;

namespace BankingAPI.Interface.contracts
{
    public interface IPaymentService
    {
        Task ProcessPayment(ProcessPaymentRequest processPaymentRequest,CacheDetails cacheDetails);
    }
}
