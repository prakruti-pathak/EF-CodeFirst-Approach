﻿using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;

namespace EFCodeFirstDemo.Services.Contract
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<EmployeeDto>>> GetAllEmployees();
        Task<ServiceResponse<List<EDto>>> GetAllEmployeesUsingEagerLoading();
        Task<ServiceResponse<List<EmployeeDto>>> LazyLoading();
        Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsInnerJoin();
        Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsLeftJoin();
        Task<ServiceResponse<List<EDto>>> GetEmployeesWithDepartmentsRightJoin();

        Task<ServiceResponse<EmployeeDto>> GetEmployeeById(int id);

        Task<ServiceResponse<string>> AddEmployee(AddEmployeeDto employeeDto);

        Task<ServiceResponse<string>> ModifyEmployee(EmployeeDto employeeDto);

        Task<ServiceResponse<string>> DeleteEmployee(int id);
    }
}
