using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empId;

        public DeleteEmployeeTransaction(int empId, PayrollDatabase database)
            : base (database)
        {
            this.empId = empId;
        }

        public override void Execute()
        {
            database.DeleteEmployee(empId);
        }
    }
}
