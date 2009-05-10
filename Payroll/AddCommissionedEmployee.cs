using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;
        private readonly double commission;

        public AddCommissionedEmployee(int empId, string name, string address, double salary, double commission, PayrollDatabase database)
            : base(empId, name, address, database)
        {
            this.salary = salary;
            this.commission = commission;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(salary, commission);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiWeeklySchedule();
        }
    }
}
