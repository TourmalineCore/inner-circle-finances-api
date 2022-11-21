using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Commands;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeFinanceService _employeeFinanceService;
        private readonly DeleteEmployeeProfileInfoCommandHandler _deleteEmployeeProfileInfoCommandHandler;

        public EmployeeController(EmployeeFinanceService employeeService,
            DeleteEmployeeProfileInfoCommandHandler deleteEmployeeProfileInfoCommandHandler)
        {
            _employeeFinanceService = employeeService;
            _deleteEmployeeProfileInfoCommandHandler = deleteEmployeeProfileInfoCommandHandler;
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

        [HttpDelete("delete-employee/{EmployeeProfileId}")]
        public Task DeleteEmployee([FromRoute] DeleteEmployeeProfileInfoCommand deleteEmployeeProfileInfoCommand)
        {
            return _deleteEmployeeProfileInfoCommandHandler.Handle(deleteEmployeeProfileInfoCommand);
        }
    }
}
