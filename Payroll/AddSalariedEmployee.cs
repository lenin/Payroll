using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;

        public AddSalariedEmployee(int empId, string name, string address, double salary, PayrollDatabase database)
            : base(empId, name, address, database)
        {
            this.salary = salary;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification(salary);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}
