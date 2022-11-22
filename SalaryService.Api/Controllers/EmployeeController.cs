using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Commands;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeFinanceService _employeeFinanceService;

        public EmployeeController(EmployeeFinanceService employeeService)
        {
            _employeeFinanceService = employeeService;
        }
        
        [HttpPost("create-employee")]
        public Task CreateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeFinanceService.CreateEmployee(salaryServiceParameters);
        }

        [HttpPost("update-employee")]
        public Task UpdateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeFinanceService.UpdateEmployee(salaryServiceParameters);
        }

        [HttpDelete("delete-employee/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeFinanceService.DeleteEmployee(id);
        }
    }
}
