using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class BiWeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime date, Paycheck lastPaycheck)
        {
            return date.DayOfWeek == DayOfWeek.Friday &&
                (lastPaycheck == null ||
                GetPayPeriodStartDate(date).Equals(lastPaycheck.PayPeriodEndDate));
        }

        public DateTime GetPayPeriodStartDate(DateTime endDate)
        {
            return endDate.AddDays(-12);
        }
    }
}
