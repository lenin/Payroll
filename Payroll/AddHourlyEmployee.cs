using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double rate;

        public AddHourlyEmployee(int empId, string name, string address, double rate, PayrollDatabase database)
            : base(empId, name, address, database)
        {
            this.rate = rate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(rate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
