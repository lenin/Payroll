using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface PaymentMethod
    {
        void Pay(Paycheck pc);
    }
}
