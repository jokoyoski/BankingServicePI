using AutoMapper;
using Common;
using Common.Model;
using Common.Utilities;

namespace BankingAPI.PaymentGateWay.Implementation
{
    public class PremiumPaymentService : IPremiumPaymentService
    {
        public string ProcessPayment(bool expensiveAvialability, int processCount)
        {
            
            if (processCount % 19 == 0) //for every 20 transactions, return one failure response
            {
                return Constants.Failure;
            }
            int value = processCount % 9;   //for every 10 transactions done , return one pending
            switch (value)
            {
                case 0:
                    return Constants.Pending;
                    break;

                default:
                    return Constants.Processed;
                    break;
            }

        }
    }
}
