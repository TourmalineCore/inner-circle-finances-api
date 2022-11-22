using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;

namespace SalaryService.Api.Controllers
{
    [Route("api/finances")]
    public class FinanceController : Controller
    {
        private readonly GetEmployeeProfileInfoQueryHandler _getEmployeeProfileInfoByIdQueryHandler;
        private readonly GetEmployeeContactInfoQueryHandler _getEmployeeContactInfoByIdQueryHandler;
        private readonly GetSEOAnalyticsInfoQueryHandler _getSeoAnalyticsInfoByIdQueryHandler;
        private readonly GetEmployeeContactInfoListQueryHandler _getEmployeeContactInfoListQueryHandler;
        private readonly GetSEOAnalyticsInfoListQueryHandler _seoAnalyticsInfoListQueryHandler;

        public FinanceController(GetEmployeeProfileInfoQueryHandler getEmployeeProfileInfoByIdQueryHandler, 
            GetEmployeeContactInfoQueryHandler getEmployeeContactInfoByIdQueryHandler, 
            GetSEOAnalyticsInfoQueryHandler getSeoAnalyticsInfoByIdQueryHandler, 
            GetEmployeeContactInfoListQueryHandler getEmployeeContactInfoListQueryHandler, 
            GetSEOAnalyticsInfoListQueryHandler seoAnalyticsInfoListQueryHandler)
        {
            _getEmployeeProfileInfoByIdQueryHandler = getEmployeeProfileInfoByIdQueryHandler;
            _getEmployeeContactInfoByIdQueryHandler = getEmployeeContactInfoByIdQueryHandler;
            _getSeoAnalyticsInfoByIdQueryHandler = getSeoAnalyticsInfoByIdQueryHandler;
            _getEmployeeContactInfoListQueryHandler = getEmployeeContactInfoListQueryHandler;
            _seoAnalyticsInfoListQueryHandler = seoAnalyticsInfoListQueryHandler;
        }

        [HttpGet("get-profile/{EmployeeId}")]
        public Task<EmployeeProfileDto> GetEmployeeProfileInformation([FromRoute] GetEmployeeProfileInfoQuery getEmployeeProfileInfoQuery)
        {
            return _getEmployeeProfileInfoByIdQueryHandler.Handle(getEmployeeProfileInfoQuery);
        }

        [HttpGet("get-contacts/{EmployeeId}")]
        public Task<EmployeeContactInfoDto> GetEmployeeContactInformation([FromRoute] GetEmployeeContactInfoQuery getEmployeeContactInfoQuery)
        {
            return _getEmployeeContactInfoByIdQueryHandler.Handle(getEmployeeContactInfoQuery);
        }

        [HttpGet("get-analytics/{EmployeeId}")]
        public Task<SEOAnalyticsInformationDto> GetAnalyticsInformation([FromRoute] GetSEOAnalyticsInfoQuery getSeoAnalyticsInfoQuery)
        {
            return _getSeoAnalyticsInfoByIdQueryHandler.Handle(getSeoAnalyticsInfoQuery);
        }

        [HttpGet("get-contacts")]
        public Task<IEnumerable<EmployeeContactInfoDto>> GetEmployeeContactInformation()
        {
            return _getEmployeeContactInfoListQueryHandler.Handle();
        }

        [HttpGet("get-analytics")]
        public Task<IEnumerable<SEOAnalyticsInformationDto>> GetAnalyticsInformationList()
        {
            return _seoAnalyticsInfoListQueryHandler.Handle();
        }
    }
}
