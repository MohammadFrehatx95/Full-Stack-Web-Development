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
        public static List<Employee> employees = new List<Employee>() { 
           new Employee(){Id = 1, FirstName = "Ahmad", LastName = "Nassar", Email = "Ahmad@123.com", Position = "Developer", BirthDate = new DateTime(2000,1,25)},
           new Employee(){Id = 2, FirstName = "Layla", LastName = "Kareem", Position = "Manager", BirthDate = new DateTime(1996,10,21)},
           new Employee(){Id = 3, FirstName = "Yusef", LastName = "Faris", Position = "HR", BirthDate = new DateTime(1995,5,5)},
           new Employee(){Id = 4, FirstName = "Nadia", LastName = "Zaid", Email = "Nadia@123.com", Position = "Developer", BirthDate = new DateTime(1991,11,15)}
        };

        [HttpGet("GetByCriteria")] // Data Annotation : Method -> Api Endpoint
        public IActionResult GetByCriteria(string? position) // (?) --> Optional / Nullable
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


        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            if(id == 0)
            {
                return BadRequest("Id Value Is Invalid");
            }
            var result = employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                Position = x.Position,
                BirthDate = x.BirthDate,
                Email = x.Email
            }).FirstOrDefault(x => x.Id == id);

            if(result == null)
            {
                return NotFound("Employee Not Found");
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(SaveEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = (employees.LastOrDefault()?.Id ?? 0) + 1,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                BirthDate = employeeDto.BirthDate,
                Position = employeeDto.Position
            };
            employees.Add(employee);

            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update(SaveEmployeeDto employeeDto)
        {
            var employee = employees.FirstOrDefault(x => x.Id == employeeDto.Id);

            if(employee == null)
            {
                return NotFound("Employee Does Not Exist");
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.BirthDate = employeeDto.BirthDate;
            employee.Position = employeeDto.Position;

            return Ok();
        } 

    }



}
