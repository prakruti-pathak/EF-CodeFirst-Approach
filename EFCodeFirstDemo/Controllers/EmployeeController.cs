﻿using EFCodeFirstDemo.Dtos;
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
