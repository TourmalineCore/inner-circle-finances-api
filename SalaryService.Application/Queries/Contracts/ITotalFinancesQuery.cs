using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface ITotalFinancesQuery
{
    Task<TotalFinances?> GetTotalFinancesAsync();
}