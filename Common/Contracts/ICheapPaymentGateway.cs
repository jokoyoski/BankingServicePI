using Common;
using System.Threading.Tasks;

namespace Common
{
    public interface ICheapPaymentGateway
    {
        string ProcessPayment(bool expensiveAvailabity, int processCount);
    }
}
