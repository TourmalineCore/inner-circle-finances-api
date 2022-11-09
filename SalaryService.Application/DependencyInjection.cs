using Microsoft.Extensions.DependencyInjection;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;

namespace SalaryService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddTransient<GetEmployeeByIdQueryHandler>();
            services.AddTransient<GetEmployeeSalaryPerformanceQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeCommandHandler>();
            services.AddTransient<CreateEmployeeSalaryPerformanceCommandHandler>();
            services.AddTransient<UpdateEmployeeSalaryPerformanceCommandHandler>();

            return services;
        }
    }
}
