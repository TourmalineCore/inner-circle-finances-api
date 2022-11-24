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
        private readonly FinanceService _financeService;
        private readonly GetCEOQueryHandler _getCeoQueryHandler;
        private readonly GetEmployeesQueryHandler _getEmployeesQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        FinanceService financeService,
        GetCEOQueryHandler getCeoQueryHandler,
        GetEmployeesQueryHandler getEmployeesQueryHandler)
        {
            _employeeService = employeeService;
            _financeService = financeService;
            _getCeoQueryHandler = getCeoQueryHandler;
            _getEmployeesQueryHandler = getEmployeesQueryHandler;
        }

        [HttpGet("get-ceo")]
        public Task<CEODto> GetCEO()
        {
            return _getCeoQueryHandler.Handle();
        }

        [HttpPut("update-ceo")]
        public Task UpdateCEO([FromBody] CEOUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateCEO(employeeUpdatingParameters);
        }

        [HttpGet("get-employees")]
        public Task<IEnumerable<EmployeeDto>> GetEmployees()
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
            return _financeService.UpdateFinances(financeUpdatingParameters);
        }

        [HttpDelete("delete-employee/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
