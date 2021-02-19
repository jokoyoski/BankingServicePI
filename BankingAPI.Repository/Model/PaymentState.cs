using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingAPI.Repository.Model
{
    public class PaymentState
    {
        [Key]
        public int Id { get; set; }
        public string TransactionReference { get; set; }
        public  string State { get; set; }
    }
}
