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
            services.AddTransient<GetEmployeeSalaryParametersQueryHandler>();
            services.AddTransient<CreateEmployeeCommandHandler>();
            services.AddTransient<UpdateEmployeeCommandHandler>();

            return services;
        }
    }
}
