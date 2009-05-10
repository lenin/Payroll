using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class ServiceCharge
    {
        private readonly DateTime time;
        private readonly double charge;

        public ServiceCharge(DateTime time, double charge)
        {
            this.time = time;
            this.charge = charge;
        }

        public DateTime Time
        {
            get { return time; }
        }

        public double Amount
        {
            get { return charge; }
        }
    }
}
