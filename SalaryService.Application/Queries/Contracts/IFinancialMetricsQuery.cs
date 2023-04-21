using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface IFinancialMetricsQuery
{
    Task<IEnumerable<FinancialMetrics>> HandleAsync();
}
