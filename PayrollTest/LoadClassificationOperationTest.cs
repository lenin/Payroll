using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using Payroll;

namespace PayrollTest
{
    [TestFixture]
    public class LoadClassificationOperationTest
    {
        private Employee employee;
        private LoadClassificationOperation operation;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(567, "Bill", "23 Pine Ct");
        }

        [Test]
        public void LoadSalariedClassificationCommand()
        {
            operation = new LoadClassificationOperation(employee, "salaried", null);
            operation.Prepare();
            SqlCommand command = operation.Command;
            Assert.AreEqual("SELECT * FROM SalariedClassification " +
              "WHERE EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.Id, command.Parameters["@EmpId"].Value);
        }
        
        [Test]
        public void CreateSalariedClassificationFromRow()
        {
            operation = new LoadClassificationOperation(employee, "salaried", null);
            operation.Prepare();
            DataRow row = LoadEmployeeOperationTest.ShuntRow("Salary", "1254.25");
            operation.CreateClassification(row);

            PaymentClassification classification = operation.Classification;
            Assert.IsTrue(classification is SalariedClassification);
            SalariedClassification salariedClassification =
                classification as SalariedClassification;
            Assert.AreEqual(1254.25, salariedClassification.Salary);
        }
    }
}
