using Microsoft.AspNetCore.Mvc;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly GetEmployeePersonalInformationQueryHandler _getEmployeePersonalInformationByIdQueryHandler;
        private readonly GetEmployeeGeneralInformationQueryHandler _getEmployeeGeneralInformationByIdQueryHandler;
        private readonly GetSEOAnalyticsInformationQueryHandler _getSEOAnalyticsInformationByIdQueryHandler;
        private readonly GetEmployeeInformationListQueryHandler _getEmployeeInformationListQueryHandler;
        private readonly GetSEOAnalyticsInformationListQueryHandler _seoAnalyticsInformationListQueryHandler;
        private readonly EmployeeSalaryService _employeeSalaryService;
        public EmployeeController(GetEmployeePersonalInformationQueryHandler getEmployeePersonalInformationByIdQueryHandler,
            GetEmployeeGeneralInformationQueryHandler getEmployeeGeneralInformationByIdQueryHandler,
            GetSEOAnalyticsInformationQueryHandler getSEOAnalyticsInformationByIdQueryHandler,
            GetEmployeeInformationListQueryHandler getEmployeeInformationListQueryHandler,
            GetSEOAnalyticsInformationListQueryHandler getSeoAnalyticsInformationListQueryHandler,
            EmployeeSalaryService employeeService)
        {
            _getEmployeePersonalInformationByIdQueryHandler = getEmployeePersonalInformationByIdQueryHandler;
            _getSEOAnalyticsInformationByIdQueryHandler = getSEOAnalyticsInformationByIdQueryHandler;
            _getEmployeeInformationListQueryHandler = getEmployeeInformationListQueryHandler;
            _seoAnalyticsInformationListQueryHandler = getSeoAnalyticsInformationListQueryHandler;
            _employeeSalaryService = employeeService;
            _getEmployeeGeneralInformationByIdQueryHandler = getEmployeeGeneralInformationByIdQueryHandler;
        }
        
        [HttpGet("get-personal-information/{EmployeeId}")]
        public Task<EmployeePersonalInformationDto> GetEmployeePersonalInformation([FromRoute] GetEmployeePersonalInformationQuery getEmployeePersonalInformationQuery)
        {
            return _getEmployeePersonalInformationByIdQueryHandler.Handle(getEmployeePersonalInformationQuery);
        }

        [HttpGet("get-general-information/{EmployeeId}")]
        public Task<EmployeeGeneralInformationDto> GetEmployeeGeneralInformation([FromRoute] GetEmployeeGeneralInformationQuery getEmployeeGeneralInformationQuery)
        {
            return _getEmployeeGeneralInformationByIdQueryHandler.Handle(getEmployeeGeneralInformationQuery);
        }

        [HttpGet("get-seo-analytics-information/{EmployeeId}")]
        public Task<SEOAnalyticsInformationDto> GetSEOAnalyticsInformation([FromRoute] GetSEOAnalyticsInformationQuery getSeoAnalyticsInformationQuery)
        {
            return _getSEOAnalyticsInformationByIdQueryHandler.Handle(getSeoAnalyticsInformationQuery);
        }

        [HttpGet("get-general-information")]
        public Task<IEnumerable<EmployeeGeneralInformationDto>> GetEmployeePersonalInformation()
        {
            return _getEmployeeInformationListQueryHandler.Handle();
        }

        [HttpGet("get-seo-analytics-information")]
        public Task<IEnumerable<SEOAnalyticsInformationDto>> GetSEOAnalyticsInformationList()
        {
            return _seoAnalyticsInformationListQueryHandler.Handle();
        }

        [HttpPost("create-employee")]
        public Task CreateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeSalaryService.CreateEmployee(salaryServiceParameters);
        }

        [HttpPost("update-employee")]
        public Task UpdateEmployee([FromBody] SalaryServiceParameters salaryServiceParameters)
        {
            return _employeeSalaryService.UpdateEmployee(salaryServiceParameters);
        }
    }
}
