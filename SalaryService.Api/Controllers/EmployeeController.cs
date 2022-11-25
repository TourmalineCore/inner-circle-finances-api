using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly GetEmployeeQueryHandler _getCeoQueryHandler;
        private readonly GetEmployeesQueryHandler _getEmployeesQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeeQueryHandler getCeoQueryHandler,
        GetEmployeesQueryHandler getEmployeesQueryHandler)
        {
            _employeeService = employeeService;
            _getCeoQueryHandler = getCeoQueryHandler;
            _getEmployeesQueryHandler = getEmployeesQueryHandler;
        }

        [HttpPost("get-preview")]
        public Task<MetricsPreviewDto> GetPreview([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.GetPreviewMetrics(financeUpdatingParameters);
        }

        [HttpGet("get-profile")]
        public Task<EmployeeProfileDto> GetProfile()
        {
            return _getCeoQueryHandler.Handle();
        }

        [HttpGet("get-employees")]
        public Task<EmployeeDto> GetEmployees()
        {
            return _getEmployeesQueryHandler.Handle();
        }

        [HttpPost("create-employee")]
        public Task CreateEmployee([FromBody] EmployeeCreatingParameters employeeCreatingParameters)
        {
            return _employeeService.CreateEmployee(employeeCreatingParameters);
        }

        [HttpPut("update-employee")]
        public Task UpdateEmployee([FromBody] EmployeeUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateEmployee(employeeUpdatingParameters);
        }

        [HttpPut("update-finance")]
        public Task UpdateFinance([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.UpdateFinances(financeUpdatingParameters);
        }

        [HttpDelete("delete-employee/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
