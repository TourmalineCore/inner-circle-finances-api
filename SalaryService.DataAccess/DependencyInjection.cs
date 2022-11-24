using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EmployeeDbContext>(options =>
            {
                
                options.UseNpgsql(connectionString,
                                o => o.UseNodaTime());
            });
            services.AddScoped<EmployeeDbContext>();
            services.AddTransient<EmployeeRepository>();
            services.AddTransient<EmployeeFinanceForPayrollRepository>();
            services.AddTransient<EmployeeFinancialMetricsRepository>();
            return services;
        }
    }
}
