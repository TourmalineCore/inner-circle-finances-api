using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using SalaryService.Domain;

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

    [HttpPost("create")]
    public async Task CreateAsync([FromBody] CompensationCreateDto dto)
    {
        var employee = await _employeesService.GetByCorporateEmailAsync(User.GetCorporateEmail());

        await _compensationsService.CreateAsync(dto, employee);
    }

    [HttpGet("all")]
    public async Task<CompensationListDto> GetAllAsync()
    {
        return await _compensationsService.GetAllAsync();
    }

    [HttpGet("types")]
    public async Task<List<CompensationType>> GetTypesAsync()
    {
        return await _compensationsService.GetTypesAsync();
    }
}
