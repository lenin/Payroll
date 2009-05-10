using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double amount;
        private readonly int empId;

        public SalesReceiptTransaction(DateTime date, double amount, int empId, PayrollDatabase database)
            : base(database)
        {
            this.date = date;
            this.amount = amount;
            this.empId = empId;
        }

        public override void Execute()
        {
            Employee e = database.GetEmployee(empId);

            if (e != null)
            {
                CommissionedClassification cc = e.Classification as CommissionedClassification;

                if (cc != null)
                    cc.AddSalesReceipt(new SalesReceipt(date, amount));
                else
                    throw new InvalidOperationException(
                        "Tried to add timecard to non-hourly employee");
            }
            else
                throw new InvalidOperationException("No such employee.");
        }
    }
}
