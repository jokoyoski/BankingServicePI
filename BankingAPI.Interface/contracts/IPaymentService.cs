using BankingAPI.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAPI.Interface.contracts
{
    public interface IPaymentService
    {
        int ProcessPayment(ProcessPaymentRequest processPaymentRequest);

    }
}
