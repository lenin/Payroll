using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        private double hourlyRate;
        private Dictionary<DateTime, TimeCard> timeCards = new Dictionary<DateTime, TimeCard>();

        public HourlyClassification(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
        }

        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public void AddTimeCard(TimeCard tc)
        {
            timeCards[tc.Date] = tc;
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards.ContainsKey(date) ? timeCards[date] : null;
        }

        public double CalculatePay(Paycheck pc)
        {
            double pay = 0;
            foreach (TimeCard card in timeCards.Values)
                if (DateUtil.IsInPayPeriod(card.Date, pc.PayPeriodStartDate, pc.PayPeriodEndDate))
                    pay += CalculatePayForTimeCard(card);
            
            return pay;
        }

        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return hourlyRate * normalHours + hourlyRate * 1.5 * overtimeHours;
        }
    }
}
