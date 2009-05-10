using System.Windows.Forms;
using Payroll;
using Payroll.UI;

namespace PayrollGUI
{
    public class WindowLoader : ViewLoader
    {
        private readonly PayrollDatabase database;
        private Form lastLoadedView;

        public WindowLoader(PayrollDatabase database)
        {
            this.database = database;
        }

        public Form LastLoadedView
        {
            get { return lastLoadedView; }
        }

        public void LoadPayrollView()
        {
            PayrollWindow view = new PayrollWindow();
            PayrollPresenter presenter = new PayrollPresenter(database, this);

            view.Presenter = presenter;
            presenter.View = view;

            LoadView(view);
        }

        public void LoadAddEmployeeView(TransactionContainer transactionContainer)
        {
            AddEmployeeWindow view = new AddEmployeeWindow();
            AddEmployeePresenter presenter = new AddEmployeePresenter(
                view, transactionContainer, database);
            view.Presenter = presenter;

            LoadView(view);
        }

        private void LoadView(Form view)
        {
            view.Show();
            lastLoadedView = view;
        }
    }
}
