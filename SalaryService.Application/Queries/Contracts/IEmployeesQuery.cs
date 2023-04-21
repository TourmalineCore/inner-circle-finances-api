using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface IEmployeesQuery
{
    Task<IEnumerable<Employee>> GetEmployeesAsync(bool includeFinanceInfo = false);
}
