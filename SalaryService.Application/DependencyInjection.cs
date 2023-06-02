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
        //services.AddTransient<IEmployeesQuery, EmployeesQuery>();
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

        //services.AddTransient<IEmployeesListQueryHandler, GetEmployeesQueryHandler>();
        //services.AddTransient<GetEmployeeQueryHandler>();
        //services.AddTransient<GetEmployeesForAnalyticsQuery>();
        //services.AddTransient<GetEmployeeProfileQueryHandler>();
        //services.AddTransient<GetAnalyticQueryHandler>();
        //services.AddTransient<GetIndicatorsQueryHandler>();
        //services.AddTransient<IGetCoefficientsQueryHandler, GetCoefficientsQueryHandler>();
        //services.AddTransient<IGetWorkingPlanQueryHandler, GetWorkingPlanQueryHandler>();
        //services.AddTransient<IGetFinancialMetricsQueryHandler, GetFinancialMetricsQueryHandler>();
        //services.AddTransient<GetEmployeeContactDetailsQueryHandler>();
        //services.AddTransient<GetEmployeeFinanceForPayrollQueryHandler>();
        //services.AddTransient<CreateEmployeeCommandHandler>();
        //services.AddTransient<UpdateEmployeeInfoCommandHandler>();
        //services.AddTransient<UpdateProfileCommandHandler>();
        //services.AddTransient<UpdateFinancesCommandHandler>();
        //services.AddTransient<DeleteEmployeeCommandHandler>();
        //services.AddTransient<CreateTotalExpensesCommandHandler>();
        //services.AddTransient<CreateEstimatedFinancialEfficiencyCommandHandler>();
        //services.AddTransient<EmployeeService>();
        //services.AddTransient<FinanceAnalyticService>();
        //services.AddScoped<EmployeeUpdateParametersValidator>();
        //services.AddTransient<IClock, Clock>();

        return services;
    }
}