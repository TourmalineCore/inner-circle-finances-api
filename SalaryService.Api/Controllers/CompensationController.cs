using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private readonly CompensationCreationCommand _compensationCreationCommand;

    public CompensationController(CompensationCreationCommand compensationCreationCommand)
    {
        _compensationCreationCommand = compensationCreationCommand;
    }

    [HttpPost("create")]
    public async Task CreateAsync([FromBody] CompensationCreateDto dto)
    {
        await _compensationCreationCommand.ExecuteAsync(dto);
    }
}

//[HttpPost("get-analytics")]
//public async Task<AnalyticsTableResponse> GetAnalyticsAsync([FromBody] IEnumerable<MetricsRowDto> metricsRows)
//{
//    if (!metricsRows.Any())
//    {
//        var employees = await _employeesService.GetEmployeesForAnalytics();
//        var employeesTotalFinancialMetrics = await _financesService.CalculateEmployeesTotalFinancialMetricsAsync();

//        return new AnalyticsTableResponse(employees, employeesTotalFinancialMetrics);
//    }

//    var metricsChanges = await _financesService.CalculateAnalyticsMetricChangesAsync(metricsRows);
//    return new AnalyticsTableResponse(metricsChanges);
//}