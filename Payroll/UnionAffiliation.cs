using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        private int memberId;
        private double dues;
        private Dictionary<DateTime, ServiceCharge> serviceCharges = new Dictionary<DateTime, ServiceCharge>();

        public UnionAffiliation()
        {
        }

        public UnionAffiliation(int memberId, double dues)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        public int MemberId
        {
            get { return memberId; }
        }

        public double Dues
        {
            get { return dues; }
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            serviceCharges[sc.Time] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime time)
        {
            return serviceCharges.ContainsKey(time) ? serviceCharges[time] : null;
        }

        public double CalculateDeductions(Paycheck paycheck)
        {
            double charges = 0;
            foreach (ServiceCharge sc in serviceCharges.Values)
                if (DateUtil.IsInPayPeriod(sc.Time, paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate))
                    charges += sc.Amount;

            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);
            
            return dues * fridays + charges;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            int fridays = 0;
            for (DateTime day = payPeriodStart;
                day <= payPeriodEnd;
                day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
            }

            return fridays;
        }
    }
}
