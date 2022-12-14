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
            var mailServiceOptions = configuration.GetSection("MailOptions");
            services.Configure<MailOptions>(c => mailServiceOptions.Bind(c));

            var helpUrls = configuration.GetSection("InnerCircleServiceUrl");
            services.Configure<InnerCircleServiceUrl>(u => helpUrls.Bind(u));

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
            services.AddTransient<UpdateFinancesCommandHandler>();
            services.AddTransient<DeleteEmployeeCommandHandler>();
            services.AddTransient<CalculatePreviewMetricsCommandHandler>();
            services.AddTransient<CreateTotalExpensesCommandHandler>();
            services.AddTransient<CreateEstimatedFinancialEfficiencyCommandHandler>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<FinanceAnalyticService>();
            services.AddTransient<MailService>();
            services.AddTransient<RequestsService>();
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
