using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;
using SalaryService.Domain;

namespace SalaryService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            Coefficients.DistrictCoefficient = Convert.ToDouble(configuration.GetSection("Coefficients:DistrictCoefficient").Value);
            Coefficients.MinimumWage = Convert.ToDouble(configuration.GetSection("Coefficients:MinimumWage").Value);
            Coefficients.IncomeTaxPercent = Convert.ToDouble(configuration.GetSection("Coefficients:IncomeTaxPercent").Value);

            services.AddTransient<GetEmployeeProfileInfoQueryHandler>();
            services.AddTransient<GetEmployeeContactInfoQueryHandler>();
            services.AddTransient<GetSEOAnalyticsInfoQueryHandler>();
            services.AddTransient<GetEmployeeContactInfoListQueryHandler>();
            services.AddTransient<GetSEOAnalyticsInfoListQueryHandler>();
            services.AddTransient<CreateEmployeeProfileInfoCommandHandler>();
            services.AddTransient<UpdateEmployeeProfileInfoCommandHandler>();
            services.AddTransient<CreateEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<UpdateEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<UpdateFinancialMetricsCommandHandler>();
            services.AddTransient<CreateHistoryMetricsCommandHandler>();
            services.AddTransient<DeleteEmployeeProfileInfoCommandHandler>();
            services.AddTransient<DeleteEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<DeleteEmployeeFinancialMetricsCommandHandler>();
            services.AddTransient<EmployeeFinanceService>();
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
