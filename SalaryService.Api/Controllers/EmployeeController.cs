using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
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
        private readonly GetEmployeeProfileQueryHandler _getEmployeeProfileQueryHandler;
        private readonly IEmployeesListQueryHandler _employeesQueryHandler;
        private readonly GetEmployeeContactDetailsQueryHandler _getEmployeeContactDetailsQueryHandler;
        private readonly GetEmployeeFinanceForPayrollQueryHandler _getEmployeeFinanceForPayrollQueryHandler;

        public EmployeeController(EmployeeService employeeService,
        GetEmployeeQueryHandler getEmployeeQueryHandler,
        IEmployeesListQueryHandler employeesQueryHandler, 
        GetEmployeeContactDetailsQueryHandler getEmployeeContactDetailsQueryHandler,
        GetEmployeeFinanceForPayrollQueryHandler getEmployeeFinanceForPayrollQueryHandler,
        GetEmployeeProfileQueryHandler getEmployeeProfileQueryHandler)
        {
            _employeeService = employeeService;
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _employeesQueryHandler = employeesQueryHandler;
            _getEmployeeContactDetailsQueryHandler = getEmployeeContactDetailsQueryHandler;
            _getEmployeeFinanceForPayrollQueryHandler = getEmployeeFinanceForPayrollQueryHandler;
            _getEmployeeProfileQueryHandler = getEmployeeProfileQueryHandler;
        }

        [RequiresPermission(UserClaimsProvider.ViewPersonalProfile)]
        [HttpGet("get-profile")]
        public Task<EmployeeProfileDto> GetProfileAsync()
        {
            return _getEmployeeProfileQueryHandler.HandleAsync(User.GetCorporateEmail());
        }

        [RequiresPermission(UserClaimsProvider.ViewContacts)]
        [HttpGet("all")]
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var includeEmployeeFinanceInfo = User.HasClaim(x => x is
            {
                Type: UserClaimsProvider.PermissionClaimType,
                Value: UserClaimsProvider.ViewSalaryAndDocumentsData
            });

            return _employeesQueryHandler.HandleAsync(includeEmployeeFinanceInfo);
        }

        [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
        [HttpPut("update")]
        public Task UpdateEmployeeAsync([FromBody] EmployeeUpdateParameters employeeUpdateParameters)
        {
            return _employeeService.UpdateEmployeeAsync(employeeUpdateParameters);
        }

        [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
        [HttpGet("{employeeId:long}")]
        public Task<EmployeeDto> GetEmployeeAsync([FromRoute] long employeeId)
        {
            return _getEmployeeQueryHandler.HandleAsync(employeeId);
        }

        [RequiresPermission(UserClaimsProvider.EditPersonalProfile)]
        [HttpPut("update-profile")]
        public Task UpdateProfile([FromBody] ProfileUpdatingParameters profileUpdatingParameters)
        {
            return _employeeService.UpdateProfileAsync(profileUpdatingParameters);
        }
        
        [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
        [HttpGet("get-finance-for-payroll/{employeeId}")]
        public Task<ColleagueFinancesDto> GetFinanceForPayroll([FromRoute] long employeeId)
        {
            return _getEmployeeFinanceForPayrollQueryHandler.HandleAsync(employeeId);
        }
        
        [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
        [HttpGet("get-contact-details/{employeeId}")]
        public Task<ColleagueContactsDto> GetContactDetails([FromRoute] long employeeId)
        {
            return _getEmployeeContactDetailsQueryHandler.HandleAsync(employeeId);
        }

        [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
        [HttpDelete("delete/{id}")]
        public Task DeleteEmployee([FromRoute] long id)
        {
            return _employeeService.DeleteEmployee(id);
        }
    }
}
