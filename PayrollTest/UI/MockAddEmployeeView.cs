using Payroll.UI;

namespace PayrollTest.UI
{
    public class MockAddEmployeeView : AddEmployeeView
    {
        public bool submitEnabled;
        public int submitEnabledCount;

        public bool SubmitEnabled
        {
            set
            {
                submitEnabled = value;
                submitEnabledCount++;
            }
        }
    }
}
