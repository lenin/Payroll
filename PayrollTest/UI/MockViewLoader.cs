using Payroll.UI;

namespace PayrollTest.UI
{
    public class MockViewLoader : ViewLoader
    {
        public bool addEmployeeViewWasLoaded;
        public bool payrollViewWasLoaded;

        public void LoadPayrollView()
        {
            payrollViewWasLoaded = true;
        }

        public void LoadAddEmployeeView(TransactionContainer transactionContainer)
        {
            addEmployeeViewWasLoaded = true;
        }
    }
}
