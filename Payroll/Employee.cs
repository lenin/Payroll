using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class Employee
    {
        private int id;
        private string name;
        private string address;
        private PaymentClassification classification;
        private PaymentSchedule schedule;
        private PaymentMethod method;
        private Affiliation affiliation;
        private Paycheck lastPaycheck;

        public Employee(int empId, string name, string address)
        {
            this.id = empId;
            this.name = name;
            this.address = address;
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
        }

        public PaymentClassification Classification
        {
            get { return classification; }
            set { classification = value; }
        }

        public PaymentSchedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }

        public PaymentMethod Method
        {
            get { return method; }
            set { method = value; }
        }

        public Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }

        public bool IsPayDate(DateTime date)
        {
            return schedule.IsPayDate(date, lastPaycheck);
        }

        public DateTime GetPayPeriodStartDate(DateTime endDate)
        {
            return schedule.GetPayPeriodStartDate(endDate);
        }

        public void Payday(Paycheck paycheck)
        {
            double grossPay = classification.CalculatePay(paycheck);
            double deductions = affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            method.Pay(paycheck);
            lastPaycheck = paycheck;
        }
    }
}
