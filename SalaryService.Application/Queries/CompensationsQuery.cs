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

    public async Task<IEnumerable<Compensation>> GetCompensationsAsync(int year, int month)
    {
        return await _context
            .Compensations
            .Include(x => x.Employee)
            .Where(x => x.DateCompensation.InUtc().Month == month && x.DateCompensation.InUtc().Year == year)
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

    public async Task<List<Compensation>> GetCompensationsByIdsAsync(long[] ids)
    {
        var compensations = await _context
            .Compensations
            .Include(x => x.Employee)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        if (compensations.Count == 0)
        {
            throw new ArgumentException("All compensations not found");
        }

        if (compensations.Count != ids.Length)
        {
            throw new ArgumentException($"Couldn't find all compensations. Found items for ids: {string.Join(", ", compensations.Select(x => x.Id))}");
        }

        return compensations;
    }
}