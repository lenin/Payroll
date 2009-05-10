using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class PaydayTransaction : Transaction
    {
        private readonly DateTime payDate;
        private Dictionary<int, Paycheck> paychecks = new Dictionary<int, Paycheck>();

        public PaydayTransaction(DateTime date, PayrollDatabase database)
            : base(database)
        {
            this.payDate = date;
        }

        public override void Execute()
        {
            foreach (Employee employee in database.GetAllEmployees())
            {
                if (employee.IsPayDate(payDate))
                {
                    DateTime startDate = employee.GetPayPeriodStartDate(payDate);
                    Paycheck pc = new Paycheck(startDate, payDate);
                    paychecks[employee.Id] = pc;
                    employee.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId)
        {
            return paychecks.ContainsKey(empId) ? paychecks[empId] : null;
        }
    }
}
