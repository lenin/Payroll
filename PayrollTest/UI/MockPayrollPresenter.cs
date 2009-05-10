using Payroll;
using Payroll.UI;

namespace PayrollTest.UI
{
    public class MockPayrollPresenter : PayrollPresenter
    {
        public bool addEmployeeActionInvoked;
        public bool runTransactionsCalled;

        public MockPayrollPresenter(PayrollDatabase database, ViewLoader viewLoader)
            : base(database, viewLoader)
        {
        }

        public MockPayrollPresenter()
            : this(null, null)
        {
        }

        public override void AddEmployeeActionInvoked()
        {
            addEmployeeActionInvoked = true;
        }

        public override void RunTransactions()
        {
            runTransactionsCalled = true;
        }
    }
}
