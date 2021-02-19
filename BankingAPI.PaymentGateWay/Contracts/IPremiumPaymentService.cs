using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.PaymentGateWay.Contracts
{
    public interface IPremiumPaymentService
    {
        Task<string> ProcessPayment(ProcessPayment processPayment);
    }
}
