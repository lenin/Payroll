using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using Payroll;

namespace PayrollTest
{
    [TestFixture]
    public class LoadPaymentMethodOperationTest
    {
        private Employee employee;
        private LoadPaymentMethodOperation operation;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(567, "Bill", "23 Pine Ct");
        }

        [Test]
        public void LoadHoldMethod()
        {
            operation = new LoadPaymentMethodOperation(employee, "hold", null);
            operation.Execute();

            PaymentMethod method = operation.Method;
            Assert.IsTrue(method is HoldMethod);
        }

        [Test]
        public void LoadDirectDepositMethodCommand()
        {
            operation = new LoadPaymentMethodOperation(employee, "directdeposit", null);
            operation.Prepare();
            SqlCommand command = operation.Command;
            Assert.AreEqual("SELECT * FROM DirectDepositAccount " +
              "WHERE EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.Id, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void CreateDirectDepositMethodFromRow()
        {
            operation = new LoadPaymentMethodOperation(employee, "directdeposit", null);
            operation.Prepare();
            DataRow row = LoadEmployeeOperationTest.ShuntRow(
                "Bank,Account", "1st Bank", "0123456");
            operation.CreatePaymentMethod(row);

            PaymentMethod method = operation.Method;
            Assert.IsTrue(method is DirectDepositMethod);
            DirectDepositMethod ddMethod = method as DirectDepositMethod;
            Assert.AreEqual("1st Bank", ddMethod.Bank);
            Assert.AreEqual("0123456", ddMethod.AccountNumber);
        }

        [Test]
        public void LoadMailMethodCommand()
        {
            operation = new LoadPaymentMethodOperation(employee, "mail", null);
            operation.Prepare();
            SqlCommand command = operation.Command;
            Assert.AreEqual("SELECT * FROM PaycheckAddress " +
              "WHERE EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.Id, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void CreateMailMethodFromRow()
        {
            operation = new LoadPaymentMethodOperation(employee, "mail", null);
            operation.Prepare();
            DataRow row = LoadEmployeeOperationTest.ShuntRow(
                "Address", "23 Pine Ct");
            operation.CreatePaymentMethod(row);

            PaymentMethod method = operation.Method;
            Assert.IsTrue(method is MailMethod);
            MailMethod mailMethod = method as MailMethod;
            Assert.AreEqual("23 Pine Ct", mailMethod.Address);
        }
    }
}
