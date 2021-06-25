using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotnetCoreWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DotnetCoreDBContext _context;

        public EmployeesController(DotnetCoreDBContext context)
        {
            _context = context;
            
        }

        // GET: api/Employees
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            //await Task.Delay(3000);
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Employee;
        }
        [HttpGet("SearchEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string name)
        {
            //await Task.Delay(3000);
            return await _context.Employees.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(name.ToLower())).ToListAsync();
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> PutEmployee(int id,Employee emp)
        {
            if (emp == null)
            {
                return BadRequest();
            }

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            try
            {
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmployee", new { id = emp.EmployeeID }, emp);

            }
            catch (Exception exp)
            {
                return BadRequest(exp);
            }

        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return emp;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
