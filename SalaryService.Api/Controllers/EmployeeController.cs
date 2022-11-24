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
        private readonly GetCEOQueryHandler _getCeoQueryHandler;
        private readonly GetEmployeesQueryHandler _getEmployeesQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetCEOQueryHandler getCeoQueryHandler,
        GetEmployeesQueryHandler getEmployeesQueryHandler)
        {
            _employeeService = employeeService;
            _getCeoQueryHandler = getCeoQueryHandler;
            _getEmployeesQueryHandler = getEmployeesQueryHandler;
        }

        [HttpGet("get-ceo/{EmployeeId}")]
        public Task<CEODto> GetCEO([FromRoute] GetCEOQuery getCeoQuery)
        {
            return _getCeoQueryHandler.Handle(getCeoQuery);
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

        [HttpPut("update-ceo")]
        public Task UpdateCEO([FromBody] CEOUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateCEO(employeeUpdatingParameters);
        }

        [HttpPut("update-employee")]
        public Task UpdateEmployee([FromBody] EmployeeUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateEmployee(employeeUpdatingParameters);
        }

        [HttpDelete("delete-employee/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
