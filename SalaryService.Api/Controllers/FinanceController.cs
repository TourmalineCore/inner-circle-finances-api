using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace SalaryService.Api.Controllers
{
    [Route("api/finance")]
    public class FinanceController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly GetAnalyticQueryHandler _getAnalyticQueryHandler;

        public FinanceController(GetAnalyticQueryHandler getAnalyticQueryHandler, EmployeeService employeeService)
        {
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
            _employeeService = employeeService;
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanViewAnalyticClaim)]
        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalytic()
        {
            return _getAnalyticQueryHandler.Handle();
        }

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanViewAnalyticClaim)]
        [HttpPost("get-preview")]
        public Task<MetricsPreviewDto> GetPreview([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.GetPreviewMetrics(financeUpdatingParameters);
        }
    }
}
