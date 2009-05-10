using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime date, Paycheck lastPaycheck);

        DateTime GetPayPeriodStartDate(DateTime endDate);
    }
}
