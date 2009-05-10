using System.Data;
using System.Data.SqlClient;

namespace Payroll
{
    public class LoadEmployeeOperation
    {
        private readonly int empId;
        private readonly SqlConnection connection;
        private Employee employee;

        public LoadEmployeeOperation(int empId, SqlConnection connection)
        {
            this.empId = empId;
            this.connection = connection;
        }

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public static DataRow LoadDataFromCommand(SqlCommand command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables["table"].Rows[0];
        }

        public SqlCommand LoadEmployeeCommand
        {
            get
            {
                string SQL = "SELECT * FROM Employee WHERE EmpId = @EmpId";
                SqlCommand command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                
                return command;
            }
        }

        public void CreateEmployee(DataRow row)
        {
            string name = row["Name"].ToString();
            string address = row["Address"].ToString();
            employee = new Employee(empId, name, address);
        }

        public void Execute()
        {
            DataRow row = LoadDataFromCommand(LoadEmployeeCommand);

            CreateEmployee(row);
            AddSchedule(row);
            AddPaymentMethod(row);
            AddClassification(row);
        }

        public void AddSchedule(DataRow row)
        {
            string scheduleType = row["ScheduleType"].ToString();
            if (scheduleType.Equals("weekly"))
                employee.Schedule = new WeeklySchedule();
            else if (scheduleType.Equals("biweekly"))
                employee.Schedule = new BiWeeklySchedule();
            else if (scheduleType.Equals("monthly"))
                employee.Schedule = new MonthlySchedule();
        }

        public void AddPaymentMethod(DataRow row)
        {
            string methodCode = row["PaymentMethodType"].ToString();
            LoadPaymentMethodOperation operation =
                new LoadPaymentMethodOperation(employee, methodCode, connection);
            operation.Execute();
            employee.Method = operation.Method;
        }

        public void AddClassification(DataRow row)
        {
            string classificationCode = row["PaymentClassificationType"].ToString();
            LoadClassificationOperation operation =
                new LoadClassificationOperation(employee, classificationCode, connection);
            operation.Execute();
            employee.Classification = operation.Classification;
        }
    }
}
