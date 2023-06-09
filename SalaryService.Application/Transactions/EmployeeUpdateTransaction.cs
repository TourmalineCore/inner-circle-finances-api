using Microsoft.Extensions.Logging;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Transactions;

public class EmployeeUpdateTransaction
{
    private readonly EmployeeDbContext _context;
    private readonly RecalcFinancialMetricsCommand _recalcFinancialMetricsCommand;
    private readonly EmployeeQuery _employeeQuery;
    private readonly ICoefficientsQuery _coefficientsQuery;
    private readonly IWorkingPlanQuery _workingPlanQuery;
    private readonly IClock _clock;
    private readonly ILogger<EmployeeUpdateTransaction> _logger;

    public EmployeeUpdateTransaction(
        EmployeeDbContext context,
        RecalcFinancialMetricsCommand recalcFinancialMetricsCommand,
        ICoefficientsQuery getCoefficientsQueryHandler,
        IWorkingPlanQuery getWorkingPlanQueryHandler,
        IClock clock,
        EmployeeQuery employeeQuery, 
        ILogger<EmployeeUpdateTransaction> logger)
    {
        _context = context;
        _coefficientsQuery = getCoefficientsQueryHandler;
        _workingPlanQuery = getWorkingPlanQueryHandler;
        _clock = clock;
        _recalcFinancialMetricsCommand = recalcFinancialMetricsCommand;
        _employeeQuery = employeeQuery;
        _logger = logger;
    }

    public async Task ExecuteAsync(EmployeeUpdateDto request)
    {
        var employee = await _employeeQuery.GetEmployeeAsync(request.EmployeeId);
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await UpdateEmployeeInfoAsync(employee, request);
            await UpdateEmployeeFinancesAsync(employee, request);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateEmployeeInfoAsync(Employee employee, EmployeeUpdateDto request)
    {
        employee.Update(
            request.Phone,
            request.PersonalEmail,
            request.GitHub,
            request.GitLab,
            Instant.FromDateTimeUtc(DateTime.SpecifyKind(request.HireDate, DateTimeKind.Utc)),
            request.IsEmployedOfficially,
            request.PersonnelNumber
        );

        _context.Update(employee);
        await _context.SaveChangesAsync();
    }

    private async Task UpdateEmployeeFinancesAsync(Employee employee, EmployeeUpdateDto request)
    {
        var coefficients = await _coefficientsQuery.GetCoefficientsAsync();
        var workingPlan = await _workingPlanQuery.GetWorkingPlanAsync();
        var now = _clock.GetCurrentInstant();

        if (employee.FinancialMetrics == null)
        {
            employee.UpdateFinancialMetrics(request.FinancesForPayroll, coefficients, workingPlan, now);
            _context.Update(employee.FinancialMetrics);
        }
        else
        {
            var employeeFinancialMetricsHistoryEntry = new EmployeeFinancialMetricsHistory(employee, now);
            employee.UpdateFinancialMetrics(request.FinancesForPayroll, coefficients, workingPlan, now);
            _context.Update(employee.FinancialMetrics);
            await _context.AddAsync(employeeFinancialMetricsHistoryEntry);
        }

        await _context.SaveChangesAsync();
        await _recalcFinancialMetricsCommand.ExecuteAsync(coefficients, now);
    }
}

