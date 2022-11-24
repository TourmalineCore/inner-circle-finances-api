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
            var coefficientOptions = configuration.GetSection("CoefficientOptions");
            services.Configure<CoefficientOptions>(c => coefficientOptions.Bind(c));

            services.AddTransient<GetCEOQueryHandler>();
            services.AddTransient<GetEmployeesQueryHandler>();
            services.AddTransient<GetAnalyticQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeCommandHandler>();
            services.AddTransient<UpdateFinancesCommandHandler>();
            services.AddTransient<UpdateCEOCommandHandler>();
            services.AddTransient<DeleteEmployeeCommandHandler>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<FinanceService>();
            services.AddTransient<IClock, Clock>();
            return services;
        }
    }
}
