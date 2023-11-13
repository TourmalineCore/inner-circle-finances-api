using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface ICompensationsQuery
{
    Task<IEnumerable<Compensation>> GetCompensationsAsync();
}
