using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class CompensationsQuery : ICompensationsQuery
{
    private readonly EmployeeDbContext _context;

    public CompensationsQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Compensation>> GetCompensationsAsync()
    {
        return await _context
            .Compensations
            .Include(x => x.Employee)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Compensation>> GetPersonalCompensationsAsync(string corporateEmail)
    {
        return await _context
            .Compensations
            .Include(x => x.Employee)
            .Where(x => x.Employee.CorporateEmail == corporateEmail)
            .AsNoTracking()
            .ToListAsync();
    }
}