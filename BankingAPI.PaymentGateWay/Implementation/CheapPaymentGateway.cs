using AutoMapper;
using BankingAPI.PaymentGateWay.Contracts;
using BankingAPI.Repository.Contract;
using BankingAPI.Repository.Implementation;
using BankingAPI.Repository.Model;
using BankingAPI.Utilities;
using Common;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace BankingAPI.PaymentGateWay.Implementation
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {

       
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public CheapPaymentGateway(IMemoryCache cache,IMapper mapper)
        {
            _mapper = mapper;
            _cache = cache;
        }
       
        public async Task<string> ProcessPayment(ProcessPayment processPayment)
        {

            int processCount = _cache.Get<int>(Constants.ProcessCount);  //keeping track of the amount of transactions been processed so that it gives certian behaviour when it reaches  a parrticualaar count
            if(processCount==5 || processCount == 6) //if the process count is 5 or 6 , the status is pending
            {
                return Constants.Pendding;
            }
            int value = processCount % 2;
            switch (value)
            {
                case 0:
                    return Constants.Processed;
                break;

                default:
                    return Constants.Failure;
                break;
            }
           
            _cache.Set<int>(Constants.ProcessCount, processCount + 1);
            
           
        }
    }
}
