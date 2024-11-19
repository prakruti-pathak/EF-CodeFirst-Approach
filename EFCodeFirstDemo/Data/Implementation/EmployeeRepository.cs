using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCodeFirstDemo.Data.Implementation
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }
        //Eager Loading
        public async Task<List<Employee>> EagerLoading()
        {
            return await _appDbContext.Employees.Include(e => e.Department).ToListAsync();
        }
        /// <summary>
        /// install nuget package Microsoft.EntityFrameworkCore.Proxies
        /// add configuration and proxy in appdbcontext
        /// and virtual to navigation na dcollection
        /// add department name as lazy loading
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> LazyLoading()
        {
            var employees = await _appDbContext.Employees.ToListAsync();

            foreach (var employee in employees)
            {
                //  lazy loading
                Console.WriteLine($" Department: {employee.Department.DepartmentName}");
            }

            return employees;
        }

        public async Task<List<EDto>> GetEmployeesWithDepartmentsInnerJoin()
        {
            var result = await (from emp in _appDbContext.Employees
                                join dept in _appDbContext.Departments
                                on emp.DepartmentId equals dept.DepartmentId
                                select new EDto
                                {
                                    EmployeeId = emp.EmployeeId,
                                    FirstName = emp.FirstName,
                                    LastName = emp.LastName,
                                    Email = emp.Email,
                                    PhoneNumber = emp.PhoneNumber,
                                    DepartmentId = emp.DepartmentId,
                                    DepartmentName = dept.DepartmentName
                                }).ToListAsync();

            return result;
        }
        public async Task<List<EDto>> GetEmployeesWithDepartmentsLeftJoin()
        {
            var result = await (from emp in _appDbContext.Employees
                                join dept in _appDbContext.Departments
                                on emp.DepartmentId equals dept.DepartmentId into deptGroup
                                from dept in deptGroup.DefaultIfEmpty() 
                                select new EDto
                                {
                                    EmployeeId = emp.EmployeeId,
                                    FirstName = emp.FirstName,
                                    LastName = emp.LastName,
                                    Email = emp.Email,
                                    PhoneNumber = emp.PhoneNumber,
                                    DepartmentId = emp.DepartmentId,
                                    DepartmentName = dept == null ? "No Department" : dept.DepartmentName
                                }).ToListAsync();

            return result;
        }
        public async Task<List<EDto>> GetEmployeesWithDepartmentsRightJoin()
        {
            var result = await (from dept in _appDbContext.Departments
                                join emp in _appDbContext.Employees
                                on dept.DepartmentId equals emp.DepartmentId into empGroup
                                from emp in empGroup.DefaultIfEmpty()
                                select new EDto
                                {
                                    EmployeeId = emp == null ? 0 : emp.EmployeeId,
                                    FirstName = emp == null ? "No Employee" : emp.FirstName,
                                    LastName = emp == null ? "No Employee" : emp.LastName,
                                    Email = emp == null ? "No Email" : emp.Email,
                                    PhoneNumber = emp == null ? "No Phone" : emp.PhoneNumber,
                                    DepartmentId = dept.DepartmentId,
                                    DepartmentName = dept.DepartmentName
                                }).ToListAsync();

            return result;
        }




        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(c => c.EmployeeId == id);
        }

        public async Task<bool> InsertEmployee(Employee employee)
        {
            var result = false;
            if (employee != null)
            {
                await _appDbContext.Employees.AddAsync(employee);
                await _appDbContext.SaveChangesAsync();
                result= true;
            }
            return result;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            if (employee != null)
            {
                _appDbContext.Employees.Update(employee);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(c => c.EmployeeId == id);
            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EmployeeExistsAsync(string email)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EmployeeExistsAsync(int id, string email)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower() && c.EmployeeId != id);
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


