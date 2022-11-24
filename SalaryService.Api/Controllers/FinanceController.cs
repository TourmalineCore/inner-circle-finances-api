using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
namespace SalaryService.Api.Controllers
{
    [Route("api/finance")]
    public class FinanceController : Controller
    {
        
        private readonly GetAnalyticQueryHandler _getAnalyticQueryHandler;

        public FinanceController(GetAnalyticQueryHandler getAnalyticQueryHandler)
        {
            _getAnalyticQueryHandler = getAnalyticQueryHandler;
        }

        [HttpGet("get-analytic")]
        public Task<IEnumerable<AnalyticDto>> GetAnalytic()
        {
            return _getAnalyticQueryHandler.Handle();
        }
    }
}
