using System.Data;
using System.Data.SqlClient;

namespace Payroll
{
    public class LoadClassificationOperation
    {
        private readonly SqlConnection connection;
        private readonly Employee employee;
        private readonly string classificationCode;
        private PaymentClassification classification;
        private delegate void ClassificationCreator(DataRow row);
        private ClassificationCreator classificationCreator;
        private string tableName;

        public LoadClassificationOperation(
            Employee employee, string classificationCode, SqlConnection connection)
        {
            this.connection = connection;
            this.employee = employee;
            this.classificationCode = classificationCode;
        }

        public void Execute()
        {
            Prepare();
            DataRow row = LoadData();
            CreateClassification(row);
        }

        public void CreateClassification(DataRow row)
        {
            classificationCreator(row);
        }

        public void Prepare()
        {
            if (classificationCode.Equals("salaried"))
            {
                tableName = "SalariedClassification";
                classificationCreator = new ClassificationCreator(
                    CreateSalariedClassification);
            }
        }

        private DataRow LoadData()
        {
            if (tableName != null)
                return LoadEmployeeOperation.LoadDataFromCommand(Command);
            else
                return null;
        }

        public PaymentClassification Classification
        {
            get { return classification; }
        }

        public SqlCommand Command
        {
            get
            {
                string SQL = string.Format(
                    "SELECT * FROM {0} WHERE EmpId = @EmpId", tableName);
                SqlCommand command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@EmpId", employee.Id);

                return command;
            }
        }

        private void CreateSalariedClassification(DataRow row)
        {
            double salary = double.Parse(row["Salary"].ToString());
            classification = new SalariedClassification(salary);
        }
    }
}
