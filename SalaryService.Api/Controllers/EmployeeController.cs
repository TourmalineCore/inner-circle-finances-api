using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
        private readonly GetBasicSalaryParametersQueryHandler _getBasicSalaryParametersQueryHandler;
        private readonly EmployeeSalaryService _employeeSalaryService;
        public EmployeeController(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, 
            GetBasicSalaryParametersQueryHandler getEmployeeSalaryParametersQueryHandler,
            EmployeeSalaryService employeeService)
        {
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _getBasicSalaryParametersQueryHandler = getEmployeeSalaryParametersQueryHandler;
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

        [HttpGet("get-full-employee-info/{EmployeeId}")]
        public Task<FullEmployeeInformationDto> GetFullEmployeeInformation([FromRoute] SalaryServiceParameters parameters)
        {
            return _employeeSalaryService.GetFullEmployeeInformation(parameters);
        }

        [HttpGet("get-by-id/{EmployeeId}")]
        public async Task<EmployeeDto> GetEmployeeById([FromRoute] GetEmployeeByIdQuery getEmployeeByIdQuery)
        {
            return await _getEmployeeByIdQueryHandler.Handle(getEmployeeByIdQuery);
        }

        [HttpGet("get-salary-parameters/{EmployeeId}")]
        public Task<BasicSalaryParametersDto> GetBasicSalaryParameters([FromRoute] GetEmployeeSalaryParametersQuery getEmployeeSalaryParametersQuery)
        {
            return _getBasicSalaryParametersQueryHandler.Handle(getEmployeeSalaryParametersQuery);
        }
    }
}
