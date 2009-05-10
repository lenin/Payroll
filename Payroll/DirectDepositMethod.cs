using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class DirectDepositMethod : PaymentMethod
    {
        private string bank;
        private string accountNumber;

        public DirectDepositMethod(string bank, string accountNumber)
        {
            this.bank = bank;
            this.accountNumber = accountNumber;
        }

        public string Bank
        {
            get { return bank; }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
        }

        public void Pay(Paycheck pc)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
