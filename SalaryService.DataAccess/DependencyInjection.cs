using Microsoft.Extensions.DependencyInjection;

namespace SalaryService.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<FakeDatabase>();

            return services;
        }
    }
}
