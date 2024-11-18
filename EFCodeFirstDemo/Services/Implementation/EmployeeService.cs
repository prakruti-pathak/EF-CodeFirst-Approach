using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;
using EFCodeFirstDemo.Services.Contract;

namespace EFCodeFirstDemo.Services.Implementation
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ServiceResponse<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var response = new ServiceResponse<IEnumerable<EmployeeDto>>();
            var employees = await _employeeRepository.GetAllEmployees(); 
            if (employees != null && employees.Any())
            {
                var employeeDtos = employees.Select(employee => new EmployeeDto()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId= employee.DepartmentId,
                }).ToList();

                response.Success = true;
                response.Data = employeeDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }
            return response;
        }
        public async Task<ServiceResponse<IEnumerable<EDto>>> GetAllEmployeesUsingEagerLoading()
        {
            var response = new ServiceResponse<IEnumerable<EDto>>();
            var employees = await _employeeRepository.EagerLoading();

            if (employees != null && employees.Any())
            {
                var employeeDtos = employees.Select(employee => new EDto()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department.DepartmentName 
                }).ToList();

                response.Success = true;
                response.Data = employeeDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<IEnumerable<EDto>>> LazyLoading()
        {
            var response = new ServiceResponse<IEnumerable<EDto>>();
            var employees = await _employeeRepository.LazyLoading();

            if (employees != null && employees.Any())
            {
                var employeeDtos = employees.Select(employee => new EDto()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId = employee.DepartmentId,
                    //DepartmentName = employee.Department.DepartmentName
                }).ToList();

                response.Success = true;
                response.Data = employeeDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<IEnumerable<object>>> GetEmployeesWithDepartmentsInnerJoin()
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            var employees = await _employeeRepository.GetEmployeesWithDepartmentsInnerJoin();

            if (employees != null && employees.Any())
            {
                var employeeDtos = employees.Select(employee => new
                {
                    EmployeeId = employee.GetType().GetProperty("EmployeeId")?.GetValue(employee),
                    FirstName = employee.GetType().GetProperty("FirstName")?.GetValue(employee),
                    LastName = employee.GetType().GetProperty("LastName")?.GetValue(employee),
                    Email = employee.GetType().GetProperty("Email")?.GetValue(employee),
                    PhoneNumber = employee.GetType().GetProperty("PhoneNumber")?.GetValue(employee),
                    DepartmentName = employee.GetType().GetProperty("DepartmentName")?.GetValue(employee)
                }).ToList();

                response.Success = true;
                response.Data = employeeDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }



        public async Task<ServiceResponse<EmployeeDto>> GetEmployeeById(int id)
        {
            var response = new ServiceResponse<EmployeeDto>();
            var existingEmployee = await _employeeRepository.GetEmployeeById(id); 
            if (existingEmployee != null)
            {
                var employeeDto = new EmployeeDto()
                {
                    EmployeeId = existingEmployee.EmployeeId,
                    FirstName = existingEmployee.FirstName,
                    LastName = existingEmployee.LastName,
                    Email = existingEmployee.Email,
                    PhoneNumber = existingEmployee.PhoneNumber,
                    DepartmentId= existingEmployee.DepartmentId,
                };
                response.Success = true;
                response.Data = employeeDto;
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, try after some time.";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> AddEmployee(AddEmployeeDto employeeDto)
        {
            var response = new ServiceResponse<string>();
            if (await _employeeRepository.EmployeeExistsAsync(employeeDto.Email)) 
            {
                response.Success = false;
                response.Message = "Employee already exists.";
                return response;
            }

            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                DepartmentId = employeeDto.DepartmentId,
            };

            var result = await _employeeRepository.InsertEmployee(employee); 
            if (result)
            {
                response.Success = true;
                response.Message = "Employee saved successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try later.";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> ModifyEmployee(EmployeeDto employeeDto)
        {
            var response = new ServiceResponse<string>();
            if (await _employeeRepository.EmployeeExistsAsync(employeeDto.EmployeeId, employeeDto.Email)) 
            {
                response.Success = false;
                response.Message = "Employee already exists.";
                return response;
            }

            var existingEmployee = await _employeeRepository.GetEmployeeById(employeeDto.EmployeeId); 
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employeeDto.FirstName;
                existingEmployee.LastName = employeeDto.LastName;
                existingEmployee.Email = employeeDto.Email;
                existingEmployee.PhoneNumber = employeeDto.PhoneNumber;
                existingEmployee.DepartmentId = employeeDto.DepartmentId;

                var result = await _employeeRepository.UpdateEmployee(existingEmployee); 
                if (result)
                {
                    response.Success = true;
                    response.Message = "Employee updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Something went wrong, try after some time.";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Employee not found.";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteEmployee(int id)
        {
            var response = new ServiceResponse<string>();
            var result = await _employeeRepository.DeleteEmployee(id); 

            if (result)
            {
                response.Success = true;
                response.Message = "Employee deleted successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, try after some time.";
            }

            return response;
        }
    }
}

