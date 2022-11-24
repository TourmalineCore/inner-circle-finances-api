using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/finance")]
    public class FinanceController : Controller
    {
        private readonly FinanceService _financeService;
        private readonly GetAnalyticQueryHandler _getAnalyticQueryHandler;

        public FinanceController(FinanceService financeService,
            GetAnalyticQueryHandler getAnalyticQueryHandler)
        {
            _financeService = financeService;
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
        }

        [HttpPut("update-finance")]
        public Task UpdateFinance([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _financeService.UpdateFinances(financeUpdatingParameters);
        }
        
        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalytic()
        {
            return _getAnalyticQueryHandler.Handle();
        }
    }
}
