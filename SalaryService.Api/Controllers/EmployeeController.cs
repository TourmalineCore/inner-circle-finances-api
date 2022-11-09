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
        private readonly GetBasicSalaryParametersQueryHandler _getBasicSalaryParametersQueryHandler;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly CreateBasicSalaryParametersCommandHandler _createBasicSalaryParametersCommandHandler;
        private readonly UpdateBasicSalaryParametersCommandHandler _updateBasicSalaryParametersCommandHandler;

        public EmployeeController(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, 
            GetBasicSalaryParametersQueryHandler getEmployeeSalaryParametersQueryHandler,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler,
            CreateBasicSalaryParametersCommandHandler createEmployeeSalaryPerformanceCommandHandler,
            UpdateBasicSalaryParametersCommandHandler updateEmployeeSalaryPerformanceCommandHandler)
        {
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _getBasicSalaryParametersQueryHandler = getEmployeeSalaryParametersQueryHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _createBasicSalaryParametersCommandHandler = createEmployeeSalaryPerformanceCommandHandler;
            _updateBasicSalaryParametersCommandHandler = updateEmployeeSalaryPerformanceCommandHandler;
        }

        [HttpGet("getById/{EmployeeId}")]
        public Task<EmployeeDto> GetEmployeeById([FromRoute] GetEmployeeByIdQuery getEmployeeByIdQuery)
        {
            return _getEmployeeByIdQueryHandler.Handle(getEmployeeByIdQuery);
        }

        [HttpGet("getSalaryParameters/{EmployeeId}")]
        public Task<BasicSalaryParametersDto> GetBasicSalaryParameters([FromRoute] GetEmployeeSalaryParametersQuery getEmployeeSalaryParametersQuery)
        {
            return _getBasicSalaryParametersQueryHandler.Handle(getEmployeeSalaryParametersQuery);
        }

        [HttpPost("create-employee")]
        public Task<long> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
           return _createEmployeeCommandHandler.Handle(createEmployeeCommand);
        }

        [HttpPost("create-salary-parameters")]
        public Task<long> CreateBasicSaalryParameters([FromBody] CreateBasicSalaryParametersCommand createEmployeeSalaryPerformanceCommand)
        {
            return _createBasicSalaryParametersCommandHandler.Handle(createEmployeeSalaryPerformanceCommand);
        }

        [HttpPost("update-employee")]
        public void UpdateEmployee([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
        {
            _updateEmployeeCommandHandler.Handle(updateEmployeeCommand);
        }

        [HttpPost("update-salary-parameters")]
        public void UpdateBasicSalaryParameters([FromBody] UpdateBasicSalaryParametersCommand updateEmployeeSalaryPerformanceCommand)
        {
            _updateBasicSalaryParametersCommandHandler.Handle(updateEmployeeSalaryPerformanceCommand);
        }
    }
}
