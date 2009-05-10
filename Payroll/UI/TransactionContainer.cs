using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.UI
{
    public class TransactionContainer
    {
        public delegate void AddAction();

        private IList<Transaction> transactions = new List<Transaction>();
        private AddAction addAction;

        public TransactionContainer(AddAction action)
        {
            addAction = action;
        }

        public IList<Transaction> Transactions
        {
            get { return transactions; }
        }

        public void Add(Transaction transaction)
        {
            transactions.Add(transaction);
            if (addAction != null)
                addAction();
        }

        public void Clear()
        {
            transactions.Clear();
        }
    }
}
