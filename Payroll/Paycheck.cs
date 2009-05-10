using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class Paycheck
    {
        private DateTime startDate;
        private DateTime endDate;
        private double grossPay;
        private double deductions;
        private double netPay;

        public Paycheck(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DateTime PayPeriodStartDate
        {
            get { return startDate; }
        }

        public DateTime PayPeriodEndDate
        {
            get { return endDate; }
        }

        public double GrossPay
        {
            get { return grossPay; }
            set { grossPay = value; }
        }

        public double Deductions
        {
            get { return deductions; }
            set { deductions = value; }
        }

        public double NetPay
        {
            get { return netPay; }
            set { netPay = value; }
        }

        public string GetField(string field)
        {
            return "Hold";
        }
    }
}
