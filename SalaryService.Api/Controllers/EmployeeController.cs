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
        private readonly GetEmployeeSalaryParametersQueryHandler _getEmployeeSalaryParametersQueryHandler;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;

        public EmployeeController(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, 
            GetEmployeeSalaryParametersQueryHandler getEmployeeSalaryParametersQueryHandler,
            CreateEmployeeCommandHandler createEmployeeCommandHandler,
            UpdateEmployeeCommandHandler updateEmployeeCommandHandler)
        {
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _getEmployeeSalaryParametersQueryHandler = getEmployeeSalaryParametersQueryHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
        }

        [HttpGet("getById/{EmployeeId}")]
        public EmployeeDto GetEmployeeById([FromRoute] GetEmployeeByIdQuery getEmployeeByIdQuery)
        {
            return _getEmployeeByIdQueryHandler.Handle(getEmployeeByIdQuery);
        }

        [HttpGet("getSalaryParameters/{EmployeeId}")]
        public SalaryParametersDto GetSalaryParameters([FromRoute] GetEmployeeSalaryParametersQuery getEmployeeSalaryParametersQuery)
        {
            return _getEmployeeSalaryParametersQueryHandler.Handle(getEmployeeSalaryParametersQuery);
        }

        [HttpPost("create")]
        public void CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            _createEmployeeCommandHandler.Handle(createEmployeeCommand);
        }

        [HttpPost("update")]
        public void UpdateEmployee([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
        {
            _updateEmployeeCommandHandler.Handle(updateEmployeeCommand);
        }
    }
}
