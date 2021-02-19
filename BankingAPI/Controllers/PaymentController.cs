using BankingAPI.Interface.contracts;
using BankingAPI.Service.RequestModel;
using BankingAPI.Utilities;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankingAPI.Controllers
{
    
    [Route("api/v1/process")]
       public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly CacheDetails _cacheDetails;
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger,CacheDetails cacheDetails)
        {
            _logger = logger;
            _paymentService = paymentService;
            _cacheDetails = cacheDetails;

        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody]ProcessPaymentRequest processPaymentRequest)
        {
            try
            {
               
                if (!Helper.IsValidDate(processPaymentRequest.ExpirationDate))  // format     MM/YYYY
                {
                    return BadRequest(new { response = Constants.InvalidDate });
                }

                //security code is not required but if user passes it, the length must be equal to 3
                if (processPaymentRequest.SecurityCode!=null && processPaymentRequest.SecurityCode.Length > 3 || processPaymentRequest.SecurityCode.Length<3)
                {
                    return BadRequest(new { response = Constants.InvalidDate });
                }
                _cacheDetails.ProcessCount += 1;
                _cacheDetails.ExpensiveGatewayAvailability = _cacheDetails.ExpensiveGatewayAvailability ? false : true; ;
                await _paymentService.ProcessPayment(processPaymentRequest,_cacheDetails);
                return Ok(new { response = Constants.Processed });
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while trying to process your request", ex.Message);
                return StatusCode(500, new { response = "Error occured while trying to process your request" });
            }


        }



    }
}
