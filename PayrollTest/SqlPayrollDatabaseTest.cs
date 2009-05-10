using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using Payroll;

namespace PayrollTest
{
    [TestFixture]
    public class SqlPayrollDatabaseTest
    {
        private PayrollDatabase database;
        private SqlConnection connection;
        private Employee employee;

        [SetUp]
        public void SetUp()
        {
            database = new SqlPayrollDatabase();

            connection = new SqlConnection("Data Source=LANGULO\\ACAPULCO;" +
                "Initial Catalog=Payroll;Integrated Security=True");
            connection.Open();

            ClearAllTables();
            
            employee = new Employee(123, "George", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectDepositMethod("Bank 1", "123890");
            employee.Classification = new SalariedClassification(1000.00);
        }

        private void ClearAllTables()
        {
            ClearTable("SalariedClassification");
            ClearTable("PaycheckAddress");
            ClearTable("DirectDepositAccount");
            ClearTable("Employee");
        }

        private void ClearTable(string table)
        {
            new SqlCommand("DELETE FROM " + table, connection).ExecuteNonQuery();
        }

        [TearDown]
        public void TearDown()
        {
            connection.Close();
        }

        private DataTable LoadTable(string table)
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM " + table, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, table);
            return dataset.Tables[table];
        }

        [Test]
        public void AddEmployee()
        {
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(123, row["EmpId"]);
            Assert.AreEqual("George", row["Name"]);
            Assert.AreEqual("123 Baker St.", row["Address"]);
        }

        [Test]
        public void ScheduleGetsSaved()
        {
            CheckSavedScheduleCode(new MonthlySchedule(), "monthly");
            ClearAllTables();
            CheckSavedScheduleCode(new WeeklySchedule(), "weekly");
            ClearAllTables();
            CheckSavedScheduleCode(new BiWeeklySchedule(), "biweekly");
        }

        private void CheckSavedScheduleCode(PaymentSchedule schedule, string expectedCode)
        {
            employee.Schedule = schedule;
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            DataRow row = table.Rows[0];

            Assert.AreEqual(expectedCode, row["ScheduleType"]);
        }

        [Test]
        public void PaymentMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new HoldMethod(), "hold");
            ClearAllTables();
            CheckSavedPaymentMethodCode(
              new DirectDepositMethod("Bank -1", "0987654321"),
              "directdeposit");
            ClearAllTables();
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");
        }

        private void CheckSavedPaymentMethodCode(PaymentMethod method, string expectedCode)
        {
            employee.Method = method;
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            DataRow row = table.Rows[0];

            Assert.AreEqual(expectedCode, row["PaymentMethodType"]);
        }

        [Test]
        public void DirectDepositMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(
                new DirectDepositMethod("Bank -1", "0987654321"),
                "directdeposit");

            DataTable table = LoadTable("DirectDepositAccount");
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual("Bank -1", row["Bank"]);
            Assert.AreEqual("0987654321", row["Account"]);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void MailMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");

            DataTable table = LoadTable("PaycheckAddress");
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual("111 Maple Ct.", row["Address"]);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void SaveIsTransactional()
        {
            // Null values won't go in the database.
            DirectDepositMethod method = new DirectDepositMethod(null, null);
            employee.Method = method;
            try
            {
                database.AddEmployee(employee);
                Assert.Fail("An exception needs to occur for this test to work.");
            }
            catch (SqlException)
            {
            }

            DataTable table = LoadTable("Employee");
            Assert.AreEqual(0, table.Rows.Count);
        }

        [Test]
        public void SaveMailMethodThenHoldMethod()
        {
            employee.Method = new MailMethod("123 Baker St.");
            database.AddEmployee(employee);

            Employee employee2 = new Employee(321, "Ed", "456 Elm St.");
            employee2.Method = new HoldMethod();
            database.AddEmployee(employee2);

            DataTable table = LoadTable("PaycheckAddress");
            Assert.AreEqual(1, table.Rows.Count);
        }

        [Test]
        public void ClassificationGetsSaved()
        {
            CheckSavedClassificationCode(
                new SalariedClassification(1000.00), "salaried");
            ClearAllTables();
            CheckSavedClassificationCode(
                new HourlyClassification(100.00), "hourly");
            ClearAllTables();
            CheckSavedClassificationCode(
                new CommissionedClassification(1000.00, 10.00), "commissioned");
        }

        private void CheckSavedClassificationCode(
            PaymentClassification classification, string expectedCode)
        {
            employee.Classification = classification;
            database.AddEmployee(employee);

            DataTable table = LoadTable("Employee");
            DataRow row = table.Rows[0];

            Assert.AreEqual(expectedCode, row["PaymentClassificationType"]);
        }

        [Test]
        public void SalariedClassificationGetsSaved()
        {
            CheckSavedClassificationCode(
                new SalariedClassification(1000.00), "salaried");

            DataTable table = LoadTable("SalariedClassification");
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(1000.00, row["Salary"]);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void LoadEmployee()
        {
            employee.Schedule = new BiWeeklySchedule();
            employee.Method = new DirectDepositMethod("1st Bank", "0123456");
            employee.Classification = new SalariedClassification(5432.10);
            database.AddEmployee(employee);

            Employee loadedEmployee = database.GetEmployee(123);
            Assert.AreEqual(123, loadedEmployee.Id);
            Assert.AreEqual(employee.Name, loadedEmployee.Name);
            Assert.AreEqual(employee.Address, loadedEmployee.Address);
            PaymentSchedule schedule = loadedEmployee.Schedule;
            Assert.IsTrue(schedule is BiWeeklySchedule);

            PaymentMethod method = loadedEmployee.Method;
            Assert.IsTrue(method is DirectDepositMethod);
            DirectDepositMethod ddMethod = method as DirectDepositMethod;
            Assert.AreEqual("1st Bank", ddMethod.Bank);
            Assert.AreEqual("0123456", ddMethod.AccountNumber);

            PaymentClassification classification = loadedEmployee.Classification;
            Assert.IsTrue(classification is SalariedClassification);
            SalariedClassification salariedClassification =
                classification as SalariedClassification;
            Assert.AreEqual(5432.10, salariedClassification.Salary);
        }
    }
}
