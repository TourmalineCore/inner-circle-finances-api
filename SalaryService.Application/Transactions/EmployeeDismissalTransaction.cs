using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Transactions;

public class EmployeeDismissalTransaction
{
    private readonly EmployeeDbContext _context;
    private readonly RecalcFinancialMetricsCommand _recalcFinancialMetricsCommand;
    private readonly EmployeeQuery _employeeQuery;
    private readonly ICoefficientsQuery _coefficientsQuery;
    private readonly IClock _clock;

    public EmployeeDismissalTransaction(
        EmployeeDbContext context,
        RecalcFinancialMetricsCommand recalcFinancialMetricsCommand,
        ICoefficientsQuery getCoefficientsQueryHandler,
        IClock clock,
        EmployeeQuery employeeQuery)
    {
        _context = context;
        _coefficientsQuery = getCoefficientsQueryHandler;
        _clock = clock;
        _recalcFinancialMetricsCommand = recalcFinancialMetricsCommand;
        _employeeQuery = employeeQuery;
    }

    public async Task ExecuteAsync(long employeeId)
    {
        var employee = await _employeeQuery.GetEmployeeAsync(employeeId);
        var coefficients = await _coefficientsQuery.GetCoefficientsAsync();
        var now = _clock.GetCurrentInstant();

        using var transaction = _context.Database.BeginTransaction();
        
        try
        {
            await RemoveEmployeeAsync(employee, now);
            await _recalcFinancialMetricsCommand.ExecuteAsync(coefficients, now);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }

    private async Task RemoveEmployeeAsync(Employee employee, Instant now)
    {
        var history = new EmployeeFinancialMetricsHistory(employee, now);

        //Why Should we send time of removement?
        employee.Delete(now);

        _context.Update(employee);

        //Probably we should mark metrics as removed (can be bad idea)
        _context.Remove(employee.FinancialMetrics);
        await _context.AddAsync(history);

        await _context.SaveChangesAsync();
    }
}

