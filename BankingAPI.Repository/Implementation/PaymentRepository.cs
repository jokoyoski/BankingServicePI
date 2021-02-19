using BankingAPI.Repository.Contract;
using BankingAPI.Repository.Data;
using BankingAPI.Repository.Model;
using System.Threading.Tasks;

namespace BankingAPI.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;
        public PaymentRepository(DataContext context)
        {
            _context = context;

        }

        public async Task ProcessPayment(Payment payment)
        {
            _context.Payments.Add(payment);
           await  _context.SaveChangesAsync();
            
        }


        public async Task ProcessProcessPaymentState(PaymentState paymentState)
        {
            _context.PaymentStates.Add(paymentState);
            await _context.SaveChangesAsync();
            
        }
    }
}
