using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;

namespace EFCodeFirstDemo.Services.Contract
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<IEnumerable<EmployeeDto>>> GetAllEmployees();

        Task<ServiceResponse<EmployeeDto>> GetEmployeeById(int id);

        Task<ServiceResponse<string>> AddEmployee(AddEmployeeDto employeeDto);

        Task<ServiceResponse<string>> ModifyEmployee(EmployeeDto employeeDto);

        Task<ServiceResponse<string>> DeleteEmployee(int id);
    }
}
