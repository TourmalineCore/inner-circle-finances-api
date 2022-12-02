using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetColleaguesQueryHandler _getColleaguesQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        GetColleaguesQueryHandler getColleaguesQueryHandler)
        {
            _employeeService = employeeService;
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getColleaguesQueryHandler = getColleaguesQueryHandler;
        }

        [Authorize]
        [HttpGet("get-profile")]
        public Task<EmployeeProfileDto> GetProfile()
        {
            return _getEmployeeQueryHandler.Handle();
        }

        [Authorize]
        [HttpGet("get-colleagues")]
        public Task<ColleagueDto> GetColleagues()
        {
            return _getColleaguesQueryHandler.Handle();
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesClaim)]
        [HttpPost("create")]
        public Task CreateEmployee([FromBody] EmployeeCreatingParameters employeeCreatingParameters)
        {
            return _employeeService.CreateEmployee(employeeCreatingParameters);
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesClaim)]
        [HttpPut("update-employee-contacts")]
        public Task UpdateEmployeeContacts([FromBody] EmployeeUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateEmployee(employeeUpdatingParameters);
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesClaim)]
        [HttpPut("update-employee-finances")]
        public Task UpdateEmployeeFinances([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.UpdateFinances(financeUpdatingParameters);
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesClaim)]
        [HttpDelete("delete/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
