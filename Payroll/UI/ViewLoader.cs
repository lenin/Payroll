namespace Payroll.UI
{
    public interface ViewLoader
    {
        void LoadPayrollView();
        void LoadAddEmployeeView(TransactionContainer transactionContainer);
    }
}
