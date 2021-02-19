using System;
using System.Collections.Generic;
using System.Text;

namespace BankingAPI.Repository.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }

        public string CardHolder { get; set; }

        public DateTime ExpidrationDate { get; set; }

        public string SecurityCode { get; set; }
        public string TransactionReference { get; set; }
        public decimal Amount { get; set; }
    }
}
