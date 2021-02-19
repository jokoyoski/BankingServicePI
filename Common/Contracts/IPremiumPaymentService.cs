
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IPremiumPaymentService
    {
        string ProcessPayment( bool expensiveAvailabity, int processCount);
    }
}
