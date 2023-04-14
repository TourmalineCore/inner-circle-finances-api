using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Application.Services;
using SalaryService.Application.Validators;

namespace SalaryService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var innerCircleServiceUrl = configuration.GetSection("InnerCircleServiceUrls");
            services.Configure<InnerCircleServiceUrls>(u => innerCircleServiceUrl.Bind(u));

            services.AddTransient<IEmployeesListQueryHandler, GetEmployeesQueryHandler>();
            services.AddTransient<GetEmployeeQueryHandler>();
            services.AddTransient<GetEmployeeProfileQueryHandler>();
            services.AddTransient<GetAnalyticQueryHandler>();
            services.AddTransient<GetIndicatorsQueryHandler>();
            services.AddTransient<IGetCoefficientsQueryHandler, GetCoefficientsQueryHandler>();
            services.AddTransient<IGetWorkingPlanQueryHandler, GetWorkingPlanQueryHandler>();
            services.AddTransient<IGetFinancialMetricsQueryHandler, GetFinancialMetricsQueryHandler>();
            services.AddTransient<GetEmployeeContactDetailsQueryHandler>();
            services.AddTransient<GetEmployeeFinanceForPayrollQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeInfoCommandHandler>();
            services.AddTransient<UpdateProfileCommandHandler>();
            services.AddTransient<UpdateFinancesCommandHandler>();
            services.AddTransient<DeleteEmployeeCommandHandler>();
            services.AddTransient<CreateTotalExpensesCommandHandler>();
            services.AddTransient<CreateEstimatedFinancialEfficiencyCommandHandler>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<FinanceAnalyticService>();
            services.AddScoped<EmployeeUpdateParametersValidator>();
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
