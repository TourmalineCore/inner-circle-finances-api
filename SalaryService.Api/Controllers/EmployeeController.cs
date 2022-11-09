using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
        private readonly GetEmployeeSalaryPerformanceQueryHandler _getEmployeeSalaryParametersQueryHandler;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly CreateEmployeeSalaryPerformanceCommandHandler _createEmployeeSalaryPerformanceCommandHandler;
        private readonly UpdateEmployeeSalaryPerformanceCommandHandler _updateEmployeeSalaryPerformanceCommandHandler;

        public EmployeeController(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, 
            GetEmployeeSalaryPerformanceQueryHandler getEmployeeSalaryParametersQueryHandler,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            CreateEmployeeSalaryPerformanceCommandHandler createEmployeeSalaryPerformanceCommandHandler,
            UpdateEmployeeSalaryPerformanceCommandHandler updateEmployeeSalaryPerformanceCommandHandler)
        {
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _getEmployeeSalaryParametersQueryHandler = getEmployeeSalaryParametersQueryHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _createEmployeeSalaryPerformanceCommandHandler = createEmployeeSalaryPerformanceCommandHandler;
            _updateEmployeeSalaryPerformanceCommandHandler = updateEmployeeSalaryPerformanceCommandHandler;
        }

        [HttpGet("getById/{EmployeeId}")]
        public Task<EmployeeDto> GetEmployeeById([FromRoute] GetEmployeeByIdQuery getEmployeeByIdQuery)
        {
            return _getEmployeeByIdQueryHandler.Handle(getEmployeeByIdQuery);
        }

        [HttpGet("getSalaryParameters/{EmployeeId}")]
        public Task<SalaryParametersDto> GetSalaryPerformance([FromRoute] GetEmployeeSalaryParametersQuery getEmployeeSalaryParametersQuery)
        {
            return _getEmployeeSalaryParametersQueryHandler.Handle(getEmployeeSalaryParametersQuery);
        }

        [HttpPost("create-employee")]
        public Task<long> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
           return _createEmployeeCommandHandler.Handle(createEmployeeCommand);
        }

        [HttpPost("create-performances")]
        public Task<long> CreatePerformances([FromBody] CreateEmployeeSalaryPerformanceCommand createEmployeeSalaryPerformanceCommand)
        {
            return _createEmployeeSalaryPerformanceCommandHandler.Handle(createEmployeeSalaryPerformanceCommand);
        }

        [HttpPost("update-employee")]
        public void UpdateEmployee([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
        {
            _updateEmployeeCommandHandler.Handle(updateEmployeeCommand);
        }

        [HttpPost("update-performances")]
        public void UpdatePerformances([FromBody] UpdateEmployeeSalaryPerformanceCommand updateEmployeeSalaryPerformanceCommand)
        {
            _updateEmployeeSalaryPerformanceCommandHandler.Handle(updateEmployeeSalaryPerformanceCommand);
        }
    }
}
