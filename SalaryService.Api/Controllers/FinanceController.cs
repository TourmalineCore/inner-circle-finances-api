using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Api.Responses;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers;

[Authorize]
[Route("api/finance")]
public class FinanceController : Controller
{
    private readonly FinancesService _financesService;
    private readonly EmployeesService _employeesService;

    public FinanceController(FinancesService financesService, EmployeesService employeesService)
    {
        _financesService = financesService;
        _employeesService = employeesService;
    }

    [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
    [HttpPost("get-analytics")]
    public async Task<AnalyticsTableResponse> GetAnalyticsAsync([FromBody] IEnumerable<MetricsRowDto> metricsRows)
    {
        if (metricsRows == null || metricsRows.Count() == 0)
        {
            var employees = await _employeesService.GetAllAsync(true);
            var employeesTotalFinancialMetrics = await _financesService.CalculateEmployeesTotalFinancialMetricsAsync();

            return new AnalyticsTableResponse(employees, employeesTotalFinancialMetrics);
        }

        var metricsChanges = await _financesService.CalculateAnalyticsMetricChangesAsync(metricsRows);
        return new AnalyticsTableResponse(metricsChanges);
    }
    
    [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
    [HttpGet("get-total-finance")]
    internal async Task<IndicatorsResponse> GetIndicatorsAsync()
    {
        var coefficients = await _financesService.GetCoefficientsAsync();
        var workingPlan = await _financesService.GetWorkingPlanAsync();
        var totalFinances = await _financesService.GetTotalFinancesAsync();
        var estimatedFinancialEfficiency = await _financesService.GetEstimatedFinancialEfficiencyAsync();

        return new IndicatorsResponse(coefficients, workingPlan, totalFinances, estimatedFinancialEfficiency);
    }
}
