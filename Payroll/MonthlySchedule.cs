using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return m1 != m2;
        }

        public bool IsPayDate(DateTime payDate, Paycheck lastPaycheck)
        {
            return IsLastDayOfMonth(payDate);
        }

        public DateTime GetPayPeriodStartDate(DateTime endDate)
        {
            return endDate.AddMonths(-1);
        }
    }
}
