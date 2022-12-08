using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers
{ 
    [Authorize]
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetColleaguesQueryHandler _getColleaguesQueryHandler;
        private readonly GetEmployeeContactDetailsQueryHandler _getEmployeeContactDetailsQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        GetColleaguesQueryHandler getColleaguesQueryHandler, 
        GetEmployeeContactDetailsQueryHandler getEmployeeContactDetailsQueryHandler)
        {
            _employeeService = employeeService;
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getColleaguesQueryHandler = getColleaguesQueryHandler;
            _getEmployeeContactDetailsQueryHandler = getEmployeeContactDetailsQueryHandler;
        }
        
        [HttpGet("get-profile")]
        public Task<EmployeeProfileDto> GetProfile()
        {
            return _getEmployeeQueryHandler.HandleAsync();
        }

        [RequiresPermission(UserClaimsProvider.CanManageEmployeesPermission)]
        [HttpGet("get-contact-details/{employeeId}")]
        public Task<EmployeeProfileDto> GetProfile([FromRoute] long employeeId)
        {
            return _getEmployeeContactDetailsQueryHandler.HandleAsync(employeeId);
        }
        
        [HttpGet("get-colleagues")]
        public Task<ColleagueDto> GetColleagues()
        {
            return _getColleaguesQueryHandler.HandleAsync();
        }
        
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesPermission)]
        [HttpPost("create")]
        public Task CreateEmployee([FromBody] EmployeeCreatingParameters employeeCreatingParameters)
        {
            return _employeeService.CreateEmployee(employeeCreatingParameters);
        }
        
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesPermission)]
        [HttpPut("update-employee-contacts")]
        public Task UpdateEmployeeContacts([FromBody] EmployeeUpdatingParameters employeeUpdatingParameters)
        {
            return _employeeService.UpdateEmployee(employeeUpdatingParameters);
        }
        
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesPermission)]
        [HttpPut("update-employee-finances")]
        public Task UpdateEmployeeFinances([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.UpdateFinances(financeUpdatingParameters);
        }
        
        [RequiresPermission(UserClaimsProvider.CanManageEmployeesPermission)]
        [HttpDelete("delete/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
