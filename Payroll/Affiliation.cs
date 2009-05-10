using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    public interface Affiliation
    {
        double CalculateDeductions(Paycheck pc);
    }
}
