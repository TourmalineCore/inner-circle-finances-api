using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Api.Comparers;
using SalaryService.Api.Responses;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers;

//[Authorize]
[Route("api/compensations")]
public class CompensationController : Controller
{
    private readonly CompensationsService _compensationsService;

    public CompensationController(CompensationsService compensationsService)
    {
        _compensationsService = compensationsService;
    }

    [HttpPost("create")]
    public async Task CreateAsync([FromBody] CompensationCreateDto dto)
    {
        await _compensationsService.CreateAsync(dto);
    }


    [HttpGet("all")]
    public async Task<CompensationListDto> GetAllAsync()
    {
        return await _compensationsService.GetAllAsync();
    }

}