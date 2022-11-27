using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/finance")]
    public class FinanceController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly GetAnalyticQueryHandler _getAnalyticQueryHandler;
        private readonly GetTotalFinancesQueryHandler _getTotalFinancesQueryHandler;

        public FinanceController(GetAnalyticQueryHandler getAnalyticQueryHandler,
            EmployeeService employeeService,
            GetTotalFinancesQueryHandler getTotalFinancesQueryHandler)
        {
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
            _employeeService = employeeService;
            _getTotalFinancesQueryHandler = getTotalFinancesQueryHandler;
        }

        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalytic()
        {
            return _getAnalyticQueryHandler.Handle();
        }

        [HttpPost("get-preview")]
        public Task<MetricsPreviewDto> GetPreview([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _employeeService.GetPreviewMetrics(financeUpdatingParameters);
        }

        [HttpGet("get-total-finance")]
        public Task<TotalFinancesDto> GetTotalFinance()
        {
            return _getTotalFinancesQueryHandler.Handle();
        }
    }
}
