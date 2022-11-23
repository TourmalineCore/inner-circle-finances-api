using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeFinanceService;
        private readonly GetEmployeesByIdQueryHandler _getEmployeesByIdByIdQueryHandler;
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetEmployeesListQueryHandler _getEmployeesListQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeesByIdQueryHandler getEmployeesByIdByIdQueryHandler, 
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        GetEmployeesListQueryHandler getEmployeesListQueryHandler)
        {
            _employeeFinanceService = employeeService;
            _getEmployeesByIdByIdQueryHandler = getEmployeesByIdByIdQueryHandler;
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getEmployeesListQueryHandler = getEmployeesListQueryHandler;
        }

        [HttpGet("get-employee/{EmployeeId}")]
        public Task<EmployeeDto> GetEmployee([FromRoute] GetEmployeeQuery getEmployeeQuery)
        {
            return _getEmployeeQueryHandler.Handle(getEmployeeQuery);
        }

        [HttpGet("get-employees")]
        public Task<IEnumerable<EmployeeContactDetailsDto>> GetEmployeesContactDetailsList()
        {
            return _getEmployeesListQueryHandler.Handle();
        }

        [HttpGet("get-contact-details/{EmployeeId}")]
        public Task<EmployeeContactDetailsDto> GetEmployeesContactDetails([FromRoute] GetEmployeesQuery getEmployeesQuery)
        {
            return _getEmployeesByIdByIdQueryHandler.Handle(getEmployeesQuery);
        }

        [HttpPost("create-employee")]
        public Task CreateEmployee([FromBody] EmployeeCreatingParameters salaryServiceParameters)
        {
            return _employeeFinanceService.CreateEmployee(salaryServiceParameters);
        }

        [HttpPost("update-employee")]
        public Task UpdateEmployee([FromBody] EmployeeUpdatingParameters salaryServiceParameters)
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
