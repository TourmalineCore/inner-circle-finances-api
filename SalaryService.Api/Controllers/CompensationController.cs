using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using SalaryService.Domain;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers;

[Authorize]
[Route("api/compensations")]
public class CompensationController : Controller
{
    private readonly CompensationsService _compensationsService;
    private readonly EmployeesService _employeesService;

    public CompensationController(CompensationsService compensationsService, EmployeesService employeesService)
    {
        _compensationsService = compensationsService;
        _employeesService = employeesService;
    }

    [RequiresPermission(UserClaimsProvider.ViewPersonalCompensations)]
    [HttpPost("create")]
    public async Task CreateAsync([FromBody] CompensationCreateDto dto)
    {
        var employee = await _employeesService.GetByCorporateEmailAsync(User.GetCorporateEmail());

        await _compensationsService.CreateAsync(dto, employee);
    }

    [RequiresPermission(UserClaimsProvider.ViewPersonalCompensations)]
    [HttpGet("all")]
    public async Task<PersonalCompensationListDto> GetAllAsync()
    {
        return await _compensationsService.GetAllAsync(User.GetCorporateEmail());
    }

    [RequiresPermission(UserClaimsProvider.ViewPersonalCompensations)]
    [HttpGet("types")]
    public async Task<List<CompensationType>> GetTypesAsync()
    {
        return await _compensationsService.GetTypesAsync();
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpGet("admin/all")]
    public async Task<AllCompensationsListDto> GetAdminAllAsync([FromQuery] int year, [FromQuery] int month)
    {
        return await _compensationsService.GetAdminAllAsync(year, month);
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpPut("admin/update")]
    public async Task UpdateStatusAsync([FromBody] long[] compensationsIds)
    {
        await _compensationsService.UpdateStatusAsync(compensationsIds);
    }
}
