using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime date, Paycheck lastPaycheck)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        public DateTime GetPayPeriodStartDate(DateTime endDate)
        {
            return endDate.AddDays(-5);
        }
    }
}
