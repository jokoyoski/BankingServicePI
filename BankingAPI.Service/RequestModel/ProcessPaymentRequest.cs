using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingAPI.Service.RequestModel
{
    public class ProcessPaymentRequest
    {
        [Required, CreditCard]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage ="Card Holder is required")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Card Expiry Date is required")]
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

    }
}
