using System.Collections.Generic;

namespace Payroll
{
    public interface PayrollDatabase
    {
        void AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        void DeleteEmployee(int id);
        void AddUnionMember(int memberId, Employee e);
        Employee GetUnionMember(int memberId);
        void RemoveUnionMember(int memberId);
        Dictionary<int, Employee>.ValueCollection GetAllEmployees();
    }
}
