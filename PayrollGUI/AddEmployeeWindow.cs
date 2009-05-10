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
    public partial class AddEmployeeWindow : Form, AddEmployeeView
    {
        private AddEmployeePresenter presenter;

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        public bool SubmitEnabled
        {
            set { submitButton.Enabled = value; }
        }

        public AddEmployeePresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        private int AsInt(string text)
        {
            try
            {
                return Int32.Parse(text);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private double AsDouble(string text)
        {
            try
            {
                return Double.Parse(text);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        private void empIdTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.EmpId = AsInt(empIdTextBox.Text);
        }

        private void nameTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.Name = nameTextBox.Text;
        }

        private void addressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.Address = addressTextBox.Text;
        }

        private void hourlyRateTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.HourlyRate = AsDouble(hourlyRateTextBox.Text);
        }

        private void salaryTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.Salary = AsDouble(salaryTextBox.Text);
        }

        private void commissionSalaryTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.CommissionSalary = AsDouble(commissionSalaryTextBox.Text);
        }

        private void commissionTextBox_TextChanged(object sender, System.EventArgs e)
        {
            presenter.Commission = AsDouble(commissionTextBox.Text);
        }

        private void hourlyRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            hourlyRateTextBox.Enabled = hourlyRadioButton.Checked;
            presenter.IsHourly = hourlyRadioButton.Checked;
        }

        private void salaryRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            salaryTextBox.Enabled = salaryRadioButton.Checked;
            presenter.IsSalary = salaryRadioButton.Checked;
        }

        private void commissionRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            commissionSalaryTextBox.Enabled = commissionRadioButton.Checked;
            commissionTextBox.Enabled = commissionRadioButton.Checked;
            presenter.IsCommission = commissionRadioButton.Checked;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            presenter.AddEmployee();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}