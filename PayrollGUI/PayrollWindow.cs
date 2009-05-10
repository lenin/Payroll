using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Payroll.UI;

namespace PayrollGUI
{
    public partial class PayrollWindow : Form, PayrollView
    {
        private PayrollPresenter presenter;

        public PayrollWindow()
        {
            InitializeComponent();
        }

        public string TransactionsText
        {
            set { transactionsTextBox.Text = value; }
        }

        public string EmployeesText
        {
            set { employeesTextBox.Text = value; }
        }

        public PayrollPresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        private void addEmployeeMenuItem_Click(object sender, EventArgs e)
        {
            presenter.AddEmployeeActionInvoked();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            presenter.RunTransactions();
        }
    }
}