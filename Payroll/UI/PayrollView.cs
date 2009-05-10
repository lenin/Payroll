namespace Payroll.UI
{
    public interface PayrollView
    {
        string TransactionsText { set; }
        string EmployeesText { set; }
        PayrollPresenter Presenter { set; }
    }
}
