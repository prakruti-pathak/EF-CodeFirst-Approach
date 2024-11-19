using AutoMapper;
using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;
using EFCodeFirstDemo.Services.Contract;

namespace EFCodeFirstDemo.Services.Implementation
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<EmployeeDto>>> GetAllEmployees()
        {
            var response = new ServiceResponse<List<EmployeeDto>>();
            var employees = await _employeeRepository.GetAllEmployees(); 
            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = _mapper.Map<List<EmployeeDto>>(employees);
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }
            return response;
        }
        public async Task<ServiceResponse<List<EDto>>> GetAllEmployeesUsingEagerLoading()
        {
            var response = new ServiceResponse<List<EDto>>();
            var employees = await _employeeRepository.EagerLoading();

            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = _mapper.Map<List<EDto>>(employees);
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<List<EmployeeDto>>> LazyLoading()
        {
            var response = new ServiceResponse<List<EmployeeDto>>();
            var employees = await _employeeRepository.LazyLoading();

            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = _mapper.Map<List<EmployeeDto>>(employees);
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsInnerJoin()
        {
            var response = new ServiceResponse<List<EDto>>();
            var employees = await _employeeRepository.GetEmployeesWithDepartmentsInnerJoin();

            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = employees;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsLeftJoin()
        {
            var response = new ServiceResponse<List<EDto>>();
            var employees = await _employeeRepository.GetEmployeesWithDepartmentsLeftJoin();

            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = employees;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }
        public async Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsRightJoin()
        {
            var response = new ServiceResponse<List<EDto>>();
            var employees = await _employeeRepository.GetEmployeesWithDepartmentsRightJoin();

            if (employees != null && employees.Any())
            {
                response.Success = true;
                response.Data = employees;
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
           
                response.Success = true;
                response.Data = _mapper.Map<EmployeeDto>(existingEmployee);
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

