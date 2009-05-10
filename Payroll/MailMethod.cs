using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class MailMethod : PaymentMethod
    {
        private string address;

        public MailMethod(string address)
        {
            this.address = address;
        }

        public string Address
        {
            get { return address; }
        }

        public void Pay(Paycheck pc)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
