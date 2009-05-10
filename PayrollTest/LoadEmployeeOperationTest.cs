using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using Payroll;

namespace PayrollTest
{
    [TestFixture]
    public class LoadEmployeeOperationTest
    {
        private LoadEmployeeOperation operation;
        private Employee employee;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(123, "Jean", "10 Rue de Roi");
            operation = new LoadEmployeeOperation(123, null);

            operation.Employee = employee;
        }

        [Test]
        public void LoadingEmployeeDataCommand()
        {
            SqlCommand command = operation.LoadEmployeeCommand;
            Assert.AreEqual("SELECT * FROM Employee WHERE EmpId = @EmpId",
                command.CommandText);
            Assert.AreEqual(123, command.Parameters["@EmpId"].Value);
        }

        public static DataRow ShuntRow(string columns, params object[] values)
        {
            DataTable table = new DataTable();
            foreach (string columnName in columns.Split(','))
                table.Columns.Add(columnName);
            return table.Rows.Add(values);
        }

        [Test]
        public void LoadEmployeeData()
        {
            DataRow row = ShuntRow("Name,Address", "Jean", "10 Rue de Roi");
            operation.CreateEmployee(row);

            Assert.IsNotNull(operation.Employee);
            Assert.AreEqual("Jean", operation.Employee.Name);
            Assert.AreEqual("10 Rue de Roi", operation.Employee.Address);
        }

        [Test]
        public void LoadingSchedules()
        {
            DataRow row = ShuntRow("ScheduleType", "weekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is WeeklySchedule);

            row = ShuntRow("ScheduleType", "biweekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is BiWeeklySchedule);

            row = ShuntRow("ScheduleType", "monthly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is MonthlySchedule);
        }

        [Test]
        public void LoadingHoldMethod()
        {
            DataRow row = ShuntRow("PaymentMethodType", "hold");
            operation.AddPaymentMethod(row);
            Assert.IsTrue(employee.Method is HoldMethod);
        }
    }
}
