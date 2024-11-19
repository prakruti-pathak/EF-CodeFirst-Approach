using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;

namespace EFCodeFirstDemo.Data.Contract
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
         Task<List<Employee>> EagerLoading();
        Task<List<Employee>> LazyLoading();
        Task<List<EDto>> GetEmployeesWithDepartmentsInnerJoin();
        Task<List<EDto>> GetEmployeesWithDepartmentsLeftJoin();
        Task<List<EDto>> GetEmployeesWithDepartmentsRightJoin();

          Task<Employee?> GetEmployeeById(int id);

          Task<bool> InsertEmployee(Employee employee);

          Task<bool> UpdateEmployee(Employee employee);

          Task<bool> DeleteEmployee(int id);

          Task<bool> EmployeeExistsAsync(string email);

          Task<bool> EmployeeExistsAsync(int id, string email);
    }
}
