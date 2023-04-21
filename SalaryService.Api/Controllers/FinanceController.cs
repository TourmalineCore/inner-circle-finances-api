using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Api.Responses;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers
{
    [Authorize]
    [Route("api/finance")]
    public class FinanceController : Controller
    {
        private readonly AnalyticsQuery _analyticsQuery;
        private readonly FinancesService _financesService;

        public FinanceController(
            AnalyticsQuery getAnalyticQueryHandler,
            FinancesService financesService)
        {
            _analyticsQuery = getAnalyticQueryHandler;
            _financesService = financesService;
        }
        
        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalyticsAsync()
        {
            return _analyticsQuery.HandleAsync();
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

        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpPost("calculate-analytics-metric-changes")]
        public async Task<AnalyticsMetricsChangesResponse> CalculateAnalyticsMetricChangesAsync([FromBody] IEnumerable<MetricsRowDto> metricsRows)
        {
            var metricsChanges = await _financesService.CalculateAnalyticsMetricChangesAsync(metricsRows);
            return new AnalyticsMetricsChangesResponse(metricsChanges);
        }
    }
}
