using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;

namespace SalaryService.Api.Controllers
{
    [Route("api/finances")]
    public class FinanceController : Controller
    {
        private readonly GetEmployeePersonalInformationQueryHandler _getEmployeePersonalInformationByIdQueryHandler;
        private readonly GetEmployeeGeneralInformationQueryHandler _getEmployeeGeneralInformationByIdQueryHandler;
        private readonly GetSEOAnalyticsInformationQueryHandler _getSEOAnalyticsInformationByIdQueryHandler;
        private readonly GetEmployeeInformationListQueryHandler _getEmployeeInformationListQueryHandler;
        private readonly GetSEOAnalyticsInformationListQueryHandler _seoAnalyticsInformationListQueryHandler;

        public FinanceController(GetEmployeePersonalInformationQueryHandler getEmployeePersonalInformationByIdQueryHandler, 
            GetEmployeeGeneralInformationQueryHandler getEmployeeGeneralInformationByIdQueryHandler, 
            GetSEOAnalyticsInformationQueryHandler getSEOAnalyticsInformationByIdQueryHandler, 
            GetEmployeeInformationListQueryHandler getEmployeeInformationListQueryHandler, 
            GetSEOAnalyticsInformationListQueryHandler seoAnalyticsInformationListQueryHandler)
        {
            _getEmployeePersonalInformationByIdQueryHandler = getEmployeePersonalInformationByIdQueryHandler;
            _getEmployeeGeneralInformationByIdQueryHandler = getEmployeeGeneralInformationByIdQueryHandler;
            _getSEOAnalyticsInformationByIdQueryHandler = getSEOAnalyticsInformationByIdQueryHandler;
            _getEmployeeInformationListQueryHandler = getEmployeeInformationListQueryHandler;
            _seoAnalyticsInformationListQueryHandler = seoAnalyticsInformationListQueryHandler;
        }

        [HttpGet("get-personal-information/{EmployeeId}")]
        public Task<EmployeeProfileDto> GetEmployeePersonalInformation([FromRoute] GetEmployeePersonalInformationQuery getEmployeePersonalInformationQuery)
        {
            return _getEmployeePersonalInformationByIdQueryHandler.Handle(getEmployeePersonalInformationQuery);
        }

        [HttpGet("get-general-information/{EmployeeId}")]
        public Task<EmployeeContactInfoDto> GetEmployeeGeneralInformation([FromRoute] GetEmployeeGeneralInformationQuery getEmployeeGeneralInformationQuery)
        {
            return _getEmployeeGeneralInformationByIdQueryHandler.Handle(getEmployeeGeneralInformationQuery);
        }

        [HttpGet("get-finance-data/{EmployeeId}")]
        public Task<SEOAnalyticsInformationDto> GetSEOAnalyticsInformation([FromRoute] GetSEOAnalyticsInformationQuery getSeoAnalyticsInformationQuery)
        {
            return _getSEOAnalyticsInformationByIdQueryHandler.Handle(getSeoAnalyticsInformationQuery);
        }

        [HttpGet("get-general-information")]
        public Task<IEnumerable<EmployeeContactInfoDto>> GetEmployeePersonalInformation()
        {
            return _getEmployeeInformationListQueryHandler.Handle();
        }

        [HttpGet("get-finance-data")]
        public Task<IEnumerable<SEOAnalyticsInformationDto>> GetSEOAnalyticsInformationList()
        {
            return _seoAnalyticsInformationListQueryHandler.Handle();
        }
    }
}
