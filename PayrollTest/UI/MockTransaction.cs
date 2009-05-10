using Payroll;

namespace PayrollTest.UI
{
    public class MockTransaction : Transaction
    {
        public bool wasExecuted;

        public MockTransaction()
            : this(null)
        {
        }

        public MockTransaction(PayrollDatabase database)
            : base(database)
        {
        }

        public override void Execute()
        {
            wasExecuted = true;
        }
    }
}
