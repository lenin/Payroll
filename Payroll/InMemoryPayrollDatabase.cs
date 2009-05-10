using System.Collections.Generic;

namespace Payroll
{
    public class InMemoryPayrollDatabase : PayrollDatabase
    {
        private static Dictionary<int, Employee> employees;
        private static Dictionary<int, Employee> unionMembers;

        public InMemoryPayrollDatabase()
        {
            employees = new Dictionary<int, Employee>();
            unionMembers = new Dictionary<int, Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            employees[employee.Id] = employee;
        }

        public Employee GetEmployee(int id)
        {
            return employees.ContainsKey(id) ? employees[id] : null;
        }

        public void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }

        public void AddUnionMember(int memberId, Employee employee)
        {
            unionMembers[memberId] = employee;
        }

        public Employee GetUnionMember(int memberId)
        {
            return unionMembers.ContainsKey(memberId) ? unionMembers[memberId] : null;
        }

        public void RemoveUnionMember(int memberId)
        {
            unionMembers.Remove(memberId);
        }

        public Dictionary<int, Employee>.ValueCollection GetAllEmployees()
        {
            return employees.Values;
        }
    }
}
