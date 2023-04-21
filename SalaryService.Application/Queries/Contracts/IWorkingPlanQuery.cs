using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface IWorkingPlanQuery
{
    Task<WorkingPlan> GetWorkingPlanAsync();
}
