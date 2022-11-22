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
            var coefficientOptions = configuration.GetSection("CoefficientOptions");
            services.Configure<CoefficientOptions>(c => coefficientOptions.Bind(c));

            services.AddTransient<GetEmployeeQueryHandler>();
            services.AddTransient<GetEmployeesByIdQueryHandler>();
            services.AddTransient<GetAnalyticByIdQueryHandler>();
            services.AddTransient<GetEmployeesListQueryHandler>();
            services.AddTransient<GetAnalyticListQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeCommandHandler>();
            services.AddTransient<CreateEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<UpdateEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<UpdateFinancialMetricsCommandHandler>();
            services.AddTransient<CreateHistoryMetricsCommandHandler>();
            services.AddTransient<DeleteEmployeeCommandHandler>();
            services.AddTransient<DeleteEmployeeFinanceForPayrollCommandHandler>();
            services.AddTransient<DeleteEmployeeFinancialMetricsCommandHandler>();
            services.AddTransient<EmployeeFinanceService>();
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
