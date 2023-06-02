using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class EstimatedFinancialEfficiencyQuery : IEstimatedFinancialEfficiencyQuery
{
    private readonly EmployeeDbContext _context;

    public EstimatedFinancialEfficiencyQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<EstimatedFinancialEfficiency?> GetEstimatedFinancialEfficiencyAsync()
    {
        return await _context.QueryableAsNoTracking<EstimatedFinancialEfficiency>().SingleOrDefaultAsync();
    }
}
