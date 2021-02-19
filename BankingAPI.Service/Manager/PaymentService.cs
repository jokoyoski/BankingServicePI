using AutoMapper;
using BankingAPI.Interface.contracts;
using BankingAPI.Repository.Contract;
using BankingAPI.Repository.Model;
using BankingAPI.Service.RequestModel;
using Common;
using Common.Model;
using Common.Utilities;
using System;
using System.Threading.Tasks;

namespace BankingAPI.Service.Manager
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentService _premiumPaymentService;
        
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ICheapPaymentGateway cheapPaymentGateway,
         IPremiumPaymentService premiumPaymentService,
            IExpensivePaymentGateway expensivePaymentGateway
            )
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _paymentRepository = paymentRepository;
            _premiumPaymentService = premiumPaymentService;
            _expensivePaymentGateway = expensivePaymentGateway;
            _mapper = mapper;
        }
        public async Task ProcessPayment(ProcessPaymentRequest processPaymentRequest,CacheDetails cacheDetails)
        {
            var payment = _mapper.Map<Payment>(processPaymentRequest);
            var paymentProcessStatus = GetPaymentProcessingStatus(processPaymentRequest,cacheDetails);
            var tranxRef = GenerateTransactionreference();
            payment.TransactionReference = tranxRef;
            await  _paymentRepository.ProcessPayment(payment);
            await _paymentRepository.ProcessProcessPaymentState( new PaymentState {
            State=paymentProcessStatus,
            TransactionReference=tranxRef
            });
        }



        public string GetPaymentProcessingStatus(ProcessPaymentRequest processPaymentRequest,CacheDetails cacheDetails)
        {
            string result = "";
           
            if (processPaymentRequest.Amount < 20)
            {
               result= _cheapPaymentGateway.ProcessPayment(cacheDetails.ExpensiveGatewayAvailability, cacheDetails.ProcessCount);
               return result;
            }
            else if (processPaymentRequest.Amount > 20 && processPaymentRequest.Amount < 500)
            {
                if (cacheDetails.ExpensiveGatewayAvailability) // if expensive payment gateway is available use it else switch to cheap gateway
                {
                    result= _expensivePaymentGateway.ProcessPayment(cacheDetails.ExpensiveGatewayAvailability, cacheDetails.ProcessCount);
                    return result;
                }
                else
                {
                   result= _cheapPaymentGateway.ProcessPayment(cacheDetails.ExpensiveGatewayAvailability, cacheDetails.ProcessCount);
                   return result;
                }
               
               
            }
            int i = 0;
           
            while (i < Constants.PremiumCount && result != Constants.Processed)   //retry three times if transaction not successful
            {
                    result = _premiumPaymentService.ProcessPayment(cacheDetails.ExpensiveGatewayAvailability, cacheDetails.ProcessCount);
            }
            return result;
        }
   
        public string GenerateTransactionreference()
        {
            // 64 character precision or 256-bits
            Random rdm = new Random();
            string hexValue = string.Empty;
            int num;

            for (int i = 0; i < 2; i++)
            {
                num = rdm.Next(0, int.MaxValue);
                hexValue += num.ToString("X8");
            }

            return  $"FB010/{hexValue}/{DateTime.Now.Day}";
        }

    }



   
}
