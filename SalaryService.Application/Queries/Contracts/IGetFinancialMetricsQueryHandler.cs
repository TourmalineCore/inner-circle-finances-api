using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts
{
    public interface IGetFinancialMetricsQueryHandler
    {
        Task<IEnumerable<EmployeeFinancialMetrics>> HandleAsync();
    }
}
