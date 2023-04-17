using SalaryService.Application.Dtos;

namespace SalaryService.Application.Queries.Contracts
{
    public interface IEmployeesListQueryHandler
    {
        Task<IEnumerable<EmployeeDto>> HandleAsync(bool includeFinanceInfo = false);
    }
}
