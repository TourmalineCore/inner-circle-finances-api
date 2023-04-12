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
        private readonly GetAnalyticQueryHandler _getAnalyticQueryHandler;
        private readonly GetIndicatorsQueryHandler _getIndicatorsQueryHandler;
        private readonly FinanceAnalyticService _financeService;

        public FinanceController(GetAnalyticQueryHandler getAnalyticQueryHandler,
            EmployeeService employeeService,
            GetIndicatorsQueryHandler getIndicatorsQueryHandler, 
            FinanceAnalyticService financeService)
        {
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
            _employeeService = employeeService;
            _getIndicatorsQueryHandler = getIndicatorsQueryHandler;
            _financeService = financeService;
        }
        
        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalytic()
        {
            return _getAnalyticQueryHandler.HandleAsync();
        }
        
        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpPost("get-preview")]
        public Task<MetricsPreviewDto> GetPreview([FromBody] GetPreviewParameters financeUpdatingParameters)
        {
            return _employeeService.GetPreviewMetrics(financeUpdatingParameters);
        }

        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpGet("get-total-finance")]
        public Task<IndicatorsDto> GetTotalFinance()
        {
            return _getIndicatorsQueryHandler.HandleAsync();
        }

        [RequiresPermission(UserClaimsProvider.CanViewAnalyticPermission)]
        [HttpPost("calculate-diff")]
        public async Task CalculateDiffAsync([FromBody] IEnumerable<EmployeeRowDto> employees)
        {
            var changing = await _financeService.CalculateEmployeeMetricsDiffAsync(employees);
        }
    }
}
