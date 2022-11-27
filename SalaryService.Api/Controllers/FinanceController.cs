using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Commands;
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
        private readonly CalculateTotalExpensesCommandHandler _calculateTotalExpensesCommandHandler;

        public FinanceController(GetAnalyticQueryHandler getAnalyticQueryHandler,
            EmployeeService employeeService, 
            CalculateTotalExpensesCommandHandler calculateTotalExpensesCommandHandler)
        {
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
            _employeeService = employeeService;
            _calculateTotalExpensesCommandHandler = calculateTotalExpensesCommandHandler;
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
            return _calculateTotalExpensesCommandHandler.Handle();
        }
    }
}
