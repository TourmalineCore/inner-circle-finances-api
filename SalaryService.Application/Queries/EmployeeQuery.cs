using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class EmployeeQuery
{
    private readonly EmployeeDbContext _context;

    public EmployeeQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> GetEmployeeAsync(long employeeId)
    {
        var employee = await _context
            .Queryable<Employee>()
            .Include(x => x.FinancialMetrics)
            .SingleAsync(x => x.Id == employeeId && x.DeletedAtUtc == null);

        if (employee == null)
        {
            throw new NullReferenceException("Employee not found");
        }

        return employee;
    }

    public async Task<Employee> GetEmployeeAsync(string corporateEmail)
    {
        var employee = await _context
            .Queryable<Employee>()
            .Include(x => x.FinancialMetrics)
            .SingleAsync(x => x.CorporateEmail == corporateEmail && x.DeletedAtUtc == null);

        if (employee == null)
        {
            throw new NullReferenceException("Employee not found");
        }

        return employee;
    }
}

