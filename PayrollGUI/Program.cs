using System;
using System.Windows.Forms;
using Payroll;

namespace PayrollGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PayrollDatabase database = new InMemoryPayrollDatabase();
            WindowLoader viewLoader = new WindowLoader(database);
            viewLoader.LoadPayrollView();

            Application.Run(viewLoader.LastLoadedView);
        }
    }
}