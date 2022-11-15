using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        
        private readonly EmployeeSalaryService _employeeSalaryService;
        public EmployeeController(EmployeeSalaryService employeeService)
        {
            _employeeSalaryService = employeeService;
        }
        
        [HttpPost("create-employee")]
        public Task CreateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeSalaryService.CreateEmployee(salaryServiceParameters);
        }

        [HttpPost("update-employee")]
        public Task UpdateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeSalaryService.UpdateEmployee(salaryServiceParameters);
        }
    }
}
