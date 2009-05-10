using System;
using System.Data;
using System.Data.SqlClient;

namespace Payroll
{
    public class SqlPayrollDatabase : PayrollDatabase
    {
        private readonly SqlConnection connection;
        
        public SqlPayrollDatabase()
        {
            connection = new SqlConnection("Data Source=LANGULO\\ACAPULCO;" +
                "Initial Catalog=Payroll;Integrated Security=True");
            connection.Open();
        }

        public void AddEmployee(Employee employee)
        {
            new SaveEmployeeOperation(employee, connection).Execute();
        }

        public Employee GetEmployee(int id)
        {
            LoadEmployeeOperation operation = new LoadEmployeeOperation(id, connection);
            operation.Execute();
            return operation.Employee;
        }

        public void DeleteEmployee(int id)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void AddUnionMember(int memberId, Employee e)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public Employee GetUnionMember(int memberId)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        public System.Collections.Generic.Dictionary<int, Employee>.ValueCollection GetAllEmployees()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }
    }
}
