using BankingAPI.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Repository.Contract
{
    public interface  IPaymentRepository
    {
        Task ProcessPayment(Payment payment);
        Task ProcessProcessPaymentState(PaymentState paymentState);
    }
}
