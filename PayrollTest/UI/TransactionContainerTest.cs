using System.Collections.Generic;
using NUnit.Framework;
using Payroll;
using Payroll.UI;

namespace PayrollTest.UI
{
    [TestFixture]
    public class TransactionContainerTest
    {
        private TransactionContainer container;
        private bool addActionCalled;
        private Transaction transaction;

        [SetUp]
        public void SetUp()
        {
            TransactionContainer.AddAction action =
                new TransactionContainer.AddAction(SillyAddAction);
            container = new TransactionContainer(action);
            transaction = new MockTransaction();
        }

        private void SillyAddAction()
        {
            addActionCalled = true;
        }

        [Test]
        public void Construction()
        {
            Assert.AreEqual(0, container.Transactions.Count);
        }

        [Test]
        public void AddingTransaction()
        {
            container.Add(transaction);

            IList<Transaction> transactions = container.Transactions;
            Assert.AreEqual(1, transactions.Count);
            Assert.AreSame(transaction, transactions[0]);
        }

        [Test]
        public void AddingTransactionTriggersDelegate()
        {
            container.Add(transaction);

            Assert.IsTrue(addActionCalled);
        }
    }
}
