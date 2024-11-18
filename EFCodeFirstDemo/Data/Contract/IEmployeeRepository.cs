using EFCodeFirstDemo.Models;

namespace EFCodeFirstDemo.Data.Contract
{
    public interface IEmployeeRepository
    {
          Task<IEnumerable<Employee>> GetAllEmployees();
         Task<IEnumerable<Employee>> EagerLoading();
        Task<IEnumerable<Employee>> LazyLoading();
        Task<IEnumerable<object>> GetEmployeesWithDepartmentsInnerJoin();

          Task<Employee?> GetEmployeeById(int id);

          Task<bool> InsertEmployee(Employee employee);

          Task<bool> UpdateEmployee(Employee employee);

          Task<bool> DeleteEmployee(int id);

          Task<bool> EmployeeExistsAsync(string email);

          Task<bool> EmployeeExistsAsync(int id, string email);
    }
}
