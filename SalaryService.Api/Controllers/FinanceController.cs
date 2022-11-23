using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;

namespace SalaryService.Api.Controllers
{
    [Route("api/finances")]
    public class FinanceController : Controller
    {
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetEmployeesByIdQueryHandler _getEmployeesByIdByIdQueryHandler;
        private readonly GetAnalyticByIdQueryHandler _getAnalyticByIdByIdQueryHandler;
        private readonly GetEmployeesListQueryHandler _getEmployeesListQueryHandler;
        private readonly GetAnalyticListQueryHandler _getAnalyticListQueryHandler;

        public FinanceController(GetEmployeeQueryHandler getEmployeeQueryHandler, 
            GetEmployeesByIdQueryHandler getEmployeesByIdByIdQueryHandler, 
            GetAnalyticByIdQueryHandler getAnalyticByIdByIdQueryHandler, 
            GetEmployeesListQueryHandler getEmployeesListQueryHandler, 
            GetAnalyticListQueryHandler getAnalyticListQueryHandler)
        {
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getEmployeesByIdByIdQueryHandler = getEmployeesByIdByIdQueryHandler;
            _getAnalyticByIdByIdQueryHandler = getAnalyticByIdByIdQueryHandler;
            _getEmployeesListQueryHandler = getEmployeesListQueryHandler;
            _getAnalyticListQueryHandler = getAnalyticListQueryHandler;
        }

        [HttpGet("get-employee/{EmployeeId}")]
        public Task<EmployeeProfileDto> GetEmployee([FromRoute] GetEmployeeQuery getEmployeeQuery)
        {
            return _getEmployeeQueryHandler.Handle(getEmployeeQuery);
        }

        [HttpGet("get-employees/{EmployeeId}")]
        public Task<EmployeeContactDetailsDto> GetEmployees([FromRoute] GetEmployeesQuery getEmployeesQuery)
        {
            return _getEmployeesByIdByIdQueryHandler.Handle(getEmployeesQuery);
        }

        [HttpGet("get-analytic/{EmployeeId}")]
        public Task<AnalyticDto> GetAnalytic([FromRoute] GetAnalyticQuery getAnalyticQuery)
        {
            return _getAnalyticByIdByIdQueryHandler.Handle(getAnalyticQuery);
        }

        [HttpGet("get-employees")]
        public Task<IEnumerable<EmployeeContactDetailsDto>> GetEmployeesList()
        {
            return _getEmployeesListQueryHandler.Handle();
        }

        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalyticList()
        {
            return _getAnalyticListQueryHandler.Handle();
        }
    }
}
