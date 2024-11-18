using EFCodeFirstDemo.Data.Contract;
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

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
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


