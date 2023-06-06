using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class CoefficientsQuery : ICoefficientsQuery
{
    private readonly EmployeeDbContext _context;

    public CoefficientsQuery(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task<CoefficientOptions> GetCoefficientsAsync()
    {
        return await _context.QueryableAsNoTracking<CoefficientOptions>().SingleAsync();
    }
}
