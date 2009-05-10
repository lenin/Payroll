using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeductions(Paycheck pc)
        {
            return 0;
        }
    }
}
