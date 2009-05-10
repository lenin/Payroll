using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        private readonly int empId;
        private readonly string name;
        private readonly string address;

        public AddEmployeeTransaction(int empId, string name, string address, PayrollDatabase database)
            : base(database)
        {
            this.empId = empId;
            this.name = name;
            this.address = address;
        }

        protected abstract PaymentClassification MakeClassification();
        
        protected abstract PaymentSchedule MakeSchedule();

        public override void Execute()
        {
            Employee e = new Employee(empId, name, address);
            e.Classification = MakeClassification();
            e.Schedule = MakeSchedule();
            e.Method = new HoldMethod();
            e.Affiliation = new NoAffiliation();

            database.AddEmployee(e);
        }
    }
}
