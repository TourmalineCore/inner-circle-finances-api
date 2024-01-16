using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface ICompensationsQuery
{
    Task<IEnumerable<Compensation>> GetCompensationsAsync();
    Task<IEnumerable<Compensation>> GetCompensationsAsync(int year, int month);
    Task<IEnumerable<Compensation>> GetPersonalCompensationsAsync(string corporateEmail);
    Task<List<Compensation>> GetCompensationsByIdsAsync(long[] ids);
}
