using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Api.Responses;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers;

[Authorize]
[Route("api/employees")]
public class EmployeeController : Controller
{
    private readonly EmployeesService _employeesService;

    public EmployeeController(EmployeesService employeeService)
    {
        _employeesService = employeeService;
    }

    [RequiresPermission(UserClaimsProvider.ViewPersonalProfile)]
    [HttpGet("get-profile")]
    public async Task<EmployeeProfileResponse> GetProfileAsync()
    {
        var employee = await _employeesService.GetByCorporateEmailAsync(User.GetCorporateEmail());
        return new EmployeeProfileResponse(employee);
    }

    [RequiresPermission(UserClaimsProvider.ViewContacts)]
    [HttpGet("all")]
    public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
    {
        var includeEmployeeFinanceInfo = User.HasClaim(x => x is
        {
            Type: UserClaimsProvider.PermissionClaimType,
            Value: UserClaimsProvider.ViewSalaryAndDocumentsData
        });

        var employees = await _employeesService.GetAllAsync(includeEmployeeFinanceInfo);
        return employees.Select(employee => new EmployeeResponse(employee));
    }

    [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
    [HttpPut("update")]
    public async Task UpdateEmployeeAsync([FromBody] EmployeeUpdateDto employeeUpdateParameters)
    {
        await _employeesService.UpdateAsync(employeeUpdateParameters);
    }

    [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
    [HttpGet("{employeeId:long}")]
    public async Task<EmployeeResponse> GetEmployeeAsync([FromRoute] long employeeId)
    {
        var employee = await _employeesService.GetByIdAsync(employeeId);
        return new EmployeeResponse(employee);
    }

    [RequiresPermission(UserClaimsProvider.EditPersonalProfile)]
    [HttpPut("update-profile")]
    public async Task UpdateProfileAsync([FromBody] ProfileUpdatingParameters profileUpdatingParameters)
    {
        await _employeesService.UpdateProfileAsync(profileUpdatingParameters);
    }

    [RequiresPermission(UserClaimsProvider.EditFullEmployeesData)]
    [HttpDelete("dismiss/{id:long}")]
    public async Task DismissalEmployeeAsync([FromRoute] long id)
    {
        await _employeesService.DismissAsync(id);
    }
}