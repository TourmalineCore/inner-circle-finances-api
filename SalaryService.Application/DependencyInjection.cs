using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Application.Services;
using SalaryService.Application.Transactions;
using SalaryService.Application.Validators;

namespace SalaryService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IEmployeesQuery, EmployeesQuery>();
        services.AddTransient<ICoefficientsQuery, CoefficientsQuery>();
        services.AddTransient<IWorkingPlanQuery, WorkingPlanQuery>();
        services.AddTransient<IFinancialMetricsQuery, FinancialMetricsQuery>();
        services.AddTransient<IEstimatedFinancialEfficiencyQuery, EstimatedFinancialEfficiencyQuery>();
        services.AddTransient<ITotalFinancesQuery, TotalFinancesQuery>();
        services.AddTransient<EmployeeCreationCommand>();
        services.AddTransient<ProfileUpdateCommand>();
        services.AddTransient<EmployeesService>();
        services.AddTransient<FinancesService>();
        services.AddScoped<EmployeeUpdateParametersValidator>();
        services.AddTransient<IClock, Clock>();

        services.AddTransient<RecalcEstimatedFinancialEfficiencyCommand>();
        services.AddTransient<RecalcFinancialMetricsCommand>();
        services.AddTransient<RecalcTotalMetricsCommand>();
        services.AddTransient<EmployeeDismissalTransaction>();
        services.AddTransient<EmployeeUpdateTransaction>();
        services.AddTransient<EmployeeQuery>();

        return services;
    }
}
