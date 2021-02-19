using AutoMapper;
using BankingAPI.Repository.Model;
using BankingAPI.Service.RequestModel;

namespace BankingAPI
{
    public class PaymentConfiguration :Profile
    {
        public PaymentConfiguration()
        {
            CreateMap<ProcessPaymentRequest, Payment>();
        }
    }
}
