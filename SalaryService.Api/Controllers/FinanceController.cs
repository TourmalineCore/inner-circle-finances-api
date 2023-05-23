using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly EmployeeService _employeeService;
        private readonly GetIndicatorsQueryHandler _getIndicatorsQueryHandler;
        private readonly GetEmployeesForAnalyticsQuery _getEmployeesForAnalyticsQuery;
        private readonly FinanceAnalyticService _financeService;

        public FinanceController(
            EmployeeService employeeService,
            GetIndicatorsQueryHandler getIndicatorsQueryHandler,
            FinanceAnalyticService financeService,
            GetEmployeesForAnalyticsQuery getEmployeesForAnalyticsQuery)
        {
            _employeeService = employeeService;
            _getIndicatorsQueryHandler = getIndicatorsQueryHandler;
            _financeService = financeService;
            _getEmployeesForAnalyticsQuery = getEmployeesForAnalyticsQuery;
        }

        [RequiresPermission(UserClaimsProvider.AccessAnalyticalForecastsPage)]
        [HttpPost("get-analytics")]
        public async Task<AnalyticsTableDto> GetAnalyticsAsync([FromBody] IEnumerable<MetricsRowDto> metricsRows)
        {
            if (metricsRows == null || metricsRows.Count() == 0)
            {
                var employees = await _getEmployeesForAnalyticsQuery.HandleAsync();
                var employeesTotalFinancialMetrics = await _financeService.CalculateEmployeesTotalFinancialMetricsAsync();

                return new AnalyticsTableDto(employees, employeesTotalFinancialMetrics);
            }

            var metricsChanges = await _financeService.CalculateAnalyticsMetricChangesAsync(metricsRows);
            return new AnalyticsTableDto(metricsChanges);
        }
        
        [RequiresPermission(UserClaimsProvider.AccessAnalyticalForecastsPage)]
        [HttpPost("get-preview")]
        public Task<MetricsPreviewDto> GetPreview([FromBody] GetPreviewParameters financeUpdatingParameters)
        {
            return _employeeService.GetPreviewMetrics(financeUpdatingParameters);
        }

        [RequiresPermission(UserClaimsProvider.AccessAnalyticalForecastsPage)]
        [HttpGet("get-total-finance")]
        public Task<IndicatorsDto> GetTotalFinance()
        {
            return _getIndicatorsQueryHandler.HandleAsync();
        }
    }
}
