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
        private readonly GetEmployeesByIdQueryHandler _getEmployeesByIdByIdQueryHandler;
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetEmployeesListQueryHandler _getEmployeesListQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeesByIdQueryHandler getEmployeesByIdByIdQueryHandler, 
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        GetEmployeesListQueryHandler getEmployeesListQueryHandler)
        {
            _employeeService = employeeService;
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
        public Task CreateEmployee([FromBody] EmployeeCreatingParameters employeeCreatingParameters)
        {
            return _employeeService.CreateEmployee(employeeCreatingParameters);
        }

        [HttpPost("update-employee")]
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
