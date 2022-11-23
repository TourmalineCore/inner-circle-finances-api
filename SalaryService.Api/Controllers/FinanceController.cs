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
        private readonly GetAnalyticByIdQueryHandler _getAnalyticByIdByIdQueryHandler;
        private readonly GetAnalyticListQueryHandler _getAnalyticListQueryHandler;

        public FinanceController(FinanceService financeService,
            
            GetAnalyticByIdQueryHandler getAnalyticByIdByIdQueryHandler, 
            
            GetAnalyticListQueryHandler getAnalyticListQueryHandler)
        {
            _financeService = financeService;
           
            _getAnalyticByIdByIdQueryHandler = getAnalyticByIdByIdQueryHandler;
            
            _getAnalyticListQueryHandler = getAnalyticListQueryHandler;
        }

        [HttpPost("update-finance")]
        public Task UpdateFinance([FromBody] FinanceUpdatingParameters financeUpdatingParameters)
        {
            return _financeService.UpdateFinances(financeUpdatingParameters);
        }

        [HttpGet("get-analytic/{EmployeeId}")]
        public Task<AnalyticDto> GetAnalytic([FromRoute] GetAnalyticQuery getAnalyticQuery)
        {
            return _getAnalyticByIdByIdQueryHandler.Handle(getAnalyticQuery);
        }
        
        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalyticList()
        {
            return _getAnalyticListQueryHandler.Handle();
        }
    }
}
