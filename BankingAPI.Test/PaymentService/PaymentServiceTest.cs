using AutoMapper;
using BankingAPI.Interface.contracts;
using BankingAPI.Repository.Contract;
using Common;
using Common.Model;
using Common.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BankingAPI.Test.PaymentService
{
    public class PaymentServiceTest
    {
      
        [Fact]
        public void  Assert_ThatTransaction_Reference_Is_Generated()
        {
            var paymentRepository = new Mock<IPaymentRepository>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>();
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object,autoMapper.Object,cheapPaymentGateway.Object,premiumPaymentGateway.Object,expensivePaymentGateway.Object);
            var result = paymentService.GenerateTransactionreference();
            bool isContain = result.Contains("FB010");
            Assert.True(isContain);

        }


       
        [Fact]
        public void Assert_That__Cheap_Payment_GateWay_Is_Called_If_Amount_Is_Less_Than_20_Pounds()
        {
           
            var paymentRepository = new Mock<IPaymentRepository>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>();
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object, autoMapper.Object, cheapPaymentGateway.Object, premiumPaymentGateway.Object, expensivePaymentGateway.Object);
            var cacheDetail = new CacheDetails
            {
                ProcessCount = 1,
                ExpensiveGatewayAvailability = false
            };
            paymentService.ProcessPayment(new Service.RequestModel.ProcessPaymentRequest
            {
                Amount=15
            },cacheDetail);
            cheapPaymentGateway.Verify(m => m.ProcessPayment(It.IsAny<bool>(), It.IsAny<int>()), Times.Once);



        }


        [Fact]
        public  void  Assert_That_Expensive_Payment_Gateway_Is_Called_If_Amount_Is_Greater_Than_20_Pounds()
        {

            var paymentRepository = new Mock<IPaymentRepository>();
            var memoryCache = new Mock<IMemoryCache>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>();
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object, autoMapper.Object, cheapPaymentGateway.Object, premiumPaymentGateway.Object, expensivePaymentGateway.Object);
            var cacheDetail = new CacheDetails
            {
                ProcessCount = 1,
                ExpensiveGatewayAvailability = true
            };
            paymentService.ProcessPayment(new Service.RequestModel.ProcessPaymentRequest
            {
                Amount = 90
            },cacheDetail);
            expensivePaymentGateway.Verify(m => m.ProcessPayment(It.IsAny<bool>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Assert_That_Cheap_Payment_Gateway_If_Expensive_Is_Not_Avialable()
        {

            var paymentRepository = new Mock<IPaymentRepository>();
            var memoryCache = new Mock<IMemoryCache>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>();
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object, autoMapper.Object, cheapPaymentGateway.Object, premiumPaymentGateway.Object, expensivePaymentGateway.Object);
            var cacheDetail = new CacheDetails
            {
                ProcessCount = 1,
                ExpensiveGatewayAvailability = false
            };
            paymentService.ProcessPayment(new Service.RequestModel.ProcessPaymentRequest
            {
                Amount = 90
            }, cacheDetail);
            cheapPaymentGateway.Verify(m => m.ProcessPayment(It.IsAny<bool>(), It.IsAny<int>()), Times.Once);

        }



        [Fact]

        public void Assert_That_Premium_Payment_Gateway_Is_Called_If_Amount_Is_Greater_Than_500_Pounds()
        {

            var paymentRepository = new Mock<IPaymentRepository>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>(MockBehavior.Strict);
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object, autoMapper.Object, cheapPaymentGateway.Object, premiumPaymentGateway.Object, expensivePaymentGateway.Object);
            var cacheDetail = new CacheDetails
            {
                ProcessCount = 1,
                ExpensiveGatewayAvailability = false
            };
            paymentService.ProcessPayment(new Service.RequestModel.ProcessPaymentRequest
            {
                Amount = 501
            },cacheDetail);
            premiumPaymentGateway.Verify(m => m.ProcessPayment(It.IsAny<bool>(), It.IsAny<int>()), Times.Once);

        }



        [Fact]

        public void Assert_That_Premium_Payment_Gateway_Is_Retried_Three_Times_If_Not_Processed()
        {

            var paymentRepository = new Mock<IPaymentRepository>();
            var autoMapper = new Mock<IMapper>();
            var cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            var expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            var premiumPaymentGateway = new Mock<IPremiumPaymentService>(MockBehavior.Strict);
            var paymentService = new BankingAPI.Service.Manager.PaymentService(paymentRepository.Object, autoMapper.Object, cheapPaymentGateway.Object, premiumPaymentGateway.Object, expensivePaymentGateway.Object);
            var cacheDetail = new CacheDetails
            {
                ProcessCount = 9,
                ExpensiveGatewayAvailability = false
            };
            paymentService.ProcessPayment(new Service.RequestModel.ProcessPaymentRequest
            {
                Amount = 501
            }, cacheDetail);
            premiumPaymentGateway.Verify(m => m.ProcessPayment(It.IsAny<bool>(), It.IsAny<int>()), Times.AtMostOnce);

        }





    }
}
