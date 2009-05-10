using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class CommissionedClassification : PaymentClassification
    {
        private double salary;
        private double commission;
        private Dictionary<DateTime, SalesReceipt> salesReceipts = new Dictionary<DateTime, SalesReceipt>();

        public CommissionedClassification(double salary, double commission)
        {
            this.salary = salary;
            this.commission = commission;
        }

        public void AddSalesReceipt(SalesReceipt sr)
        {
            salesReceipts[sr.Date] = sr;
        }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return salesReceipts.ContainsKey(date) ? salesReceipts[date] : null;
        }

        public double CalculatePay(Paycheck pc)
        {
            double pay = salary;
            foreach (SalesReceipt sr in salesReceipts.Values)
                if (DateUtil.IsInPayPeriod(sr.Date, pc.PayPeriodStartDate, pc.PayPeriodEndDate))
                    pay += sr.Amount * (commission / 100);
            
            return pay;
        }
    }
}
