using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.Application.Services;

namespace SalaryService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var innerCircleServiceUrl = configuration.GetSection("InnerCircleServiceUrls");
            services.Configure<InnerCircleServiceUrls>(u => innerCircleServiceUrl.Bind(u));

            services.AddTransient<GetColleaguesQueryHandler>();
            services.AddTransient<GetEmployeeQueryHandler>();
            services.AddTransient<GetAnalyticQueryHandler>();
            services.AddTransient<GetTotalFinancesQueryHandler>();
            services.AddTransient<GetCoefficientsQueryHandler>();
            services.AddTransient<GetFinancialMetricsQueryHandler>();
            services.AddTransient<GetEmployeeContactDetailsQueryHandler>();
            services.AddTransient<GetEmployeeFinanceForPayrollQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeCommandHandler>();
            services.AddTransient<UpdateProfileCommandHandler>();
            services.AddTransient<UpdateFinancesCommandHandler>();
            services.AddTransient<DeleteEmployeeCommandHandler>();
            services.AddTransient<CalculatePreviewMetricsCommandHandler>();
            services.AddTransient<CreateTotalExpensesCommandHandler>();
            services.AddTransient<CreateEstimatedFinancialEfficiencyCommandHandler>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<FinanceAnalyticService>();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Debug")
            {
                services.AddTransient<IRequestService, FakeRequestService>();
            }
            else
            {
                services.AddTransient<IRequestService, RequestsService>();
            }
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
