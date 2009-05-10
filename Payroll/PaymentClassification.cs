using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface PaymentClassification
    {
        double CalculatePay(Paycheck pc);
    }
}
