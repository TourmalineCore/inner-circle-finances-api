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


    //[HttpGet("all")]
    //public async Task<List<CompensationResponse>> GetAllAsync()
    //{
    //    var allCompensations = await _compensationCreationCommand.Get
    //    return await _employeesQuery.GetEmployeesAsync();


    //}

    //[HttpGet("all")]
    //public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
    //{
    //    var userIsAvailableToViewSalaryAndDocumentsData = User.IsAvailableToViewSalaryAndDocumentData();

    //    if (!userIsAvailableToViewSalaryAndDocumentsData)
    //    {
    //        var currentEmployees = await _employeesService.GetCurrentEmployeesAsync();
    //        return currentEmployees
    //            .OrderBy(employee => employee, new EmployeesComparer())
    //            .Select(employee => new EmployeeResponse(employee));
    //    }

    //    var allEmployees = await _employeesService.GetAllAsync();
    //    return allEmployees
    //        .OrderBy(employee => employee, new EmployeesComparer())
    //        .Select(employee => new EmployeeResponse(employee, true));
    //}
}