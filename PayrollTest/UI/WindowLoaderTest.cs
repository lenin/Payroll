using System.Windows.Forms;
using NUnit.Framework;
using Payroll;
using Payroll.UI;
using PayrollGUI;

namespace PayrollTest.UI
{
    [TestFixture]
    public class WindowLoaderTest
    {
        private PayrollDatabase database;
        private WindowLoader viewLoader;

        [SetUp]
        public void SetUp()
        {
            database = new InMemoryPayrollDatabase();
            viewLoader = new WindowLoader(database);
        }

        [Test]
        public void LoadPayrollView()
        {
            viewLoader.LoadPayrollView();

            Form form = viewLoader.LastLoadedView;
            Assert.IsTrue(form is PayrollWindow);
            Assert.IsTrue(form.Visible);

            PayrollWindow payrollWindow = form as PayrollWindow;
            PayrollPresenter presenter = payrollWindow.Presenter;
            Assert.IsNotNull(presenter);
            Assert.AreSame(form, presenter.View);
        }

        [Test]
        public void LoadAddEmployeeView()
        {
            viewLoader.LoadAddEmployeeView(new TransactionContainer(null));

            Form form = viewLoader.LastLoadedView;
            Assert.IsTrue(form is AddEmployeeWindow);
            Assert.IsTrue(form.Visible);

            AddEmployeeWindow addEmployeeWindow = form as AddEmployeeWindow;
            Assert.IsNotNull(addEmployeeWindow.Presenter);
        }
    }
}
