using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class SalariedClassification : PaymentClassification
    {
        private double salary;

        public SalariedClassification(double salary)
        {
            this.salary = salary;
        }

        public double Salary
        {
            get { return salary; }
        }

        public double CalculatePay(Paycheck pc)
        {
            return salary;
        }
    }
}
