using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Models;
using HRMS.Dtos.Employees;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]// Data Annotation
    [ApiController]// Data Annotation
    public class EmployeesController : ControllerBase
    {
        public List<Employee> employees = new List<Employee>() { 
            new Employee(){Id = 1, FirstName = "Ahmad", LastName = "Nassar", Email = "Ahmad@123.com", Position = "Developer", BirthDate = new DateTime(2000,1,25)},
            new Employee(){Id = 2, FirstName = "Layla", LastName = "Kareem", Position = "Manager", BirthDate = new DateTime(1996,10,21)},
            new Employee(){Id = 3, FirstName = "Yusef", LastName = "Faris", Position = "HR", BirthDate = new DateTime(1995,5,5)},
            new Employee(){Id = 4, FirstName = "Nadia", LastName = "Zaid", Email = "Nadia@123.com", Position = "Developer", BirthDate = new DateTime(1991,11,15)}
        };

        [HttpGet("GetByCriteria")] // Data Annotation : Method -> Api Endpoint
        public IActionResult GetByCriteria(string? position) // (?) --> Optinal / Nullable
        {
            var result = from employee in employees
                         where (position == null || employee.Position == position)
                         orderby employee.Id descending
                         select new EmployeeDto
                         {
                             Id = employee.Id,
                             Name = employee.FirstName + " " + employee.LastName,
                             Position = employee.Position,
                             BirthDate = employee.BirthDate,
                             Email = employee.Email
                         };

            return Ok(result);

            //return Ok(new { Name = "Ahmad", Age = 26});// 200
            //return NotFound("No Data Found");// 404
            //return BadRequest("Data Missing"); // 400
        }

    }



}
