using EFCodeFirstDemo.Dtos;
using EFCodeFirstDemo.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCodeFirstDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeService.GetAllEmployees(); 
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAllEmployeesUsingEagerLoading")]
        public async Task<IActionResult> GetAllEmployeesUsingEagerLoading()
        {
            var response = await _employeeService.GetAllEmployeesUsingEagerLoading();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAllEmployeesUsingLazyLoading")]
        public async Task<IActionResult> GetAllEmployeesUsingLazyLoading()
        {
            var response = await _employeeService.LazyLoading();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetEmployeesWithDepartmentsInnerJoin")]
        public async Task<IActionResult> GetEmployeesWithDepartmentsInnerJoin()
        {
            var response = await _employeeService.GetEmployeesWithDepartmentsInnerJoin();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetEmployeesWithDepartmentsLeftJoin")]
        public async Task<IActionResult> GetEmployeesWithDepartmentsLeftJoin()
        {
            var response = await _employeeService.GetEmployeesWithDepartmentsLeftJoin();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetEmployeesWithDepartmentsRightJoin")]
        public async Task<IActionResult> GetEmployeesWithDepartmentsRightJoin()
        {
            var response = await _employeeService.GetEmployeesWithDepartmentsRightJoin();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please enter valid data.");
            }
            else
            {
                var response = await _employeeService.GetEmployeeById(id);
                return response.Success ? Ok(response) : NotFound(response);
            }
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto addEmployee)
        {
            var response = await _employeeService.AddEmployee(addEmployee);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("ModifyEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto employeeDto)
        {
            var response = await _employeeService.ModifyEmployee(employeeDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please enter valid data.");
            }
            else
            {
                var response = await _employeeService.DeleteEmployee(id); 
                return response.Success ? Ok(response) : BadRequest(response);
            }
        }
    }
}
