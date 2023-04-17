using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts
{
    public interface IGetWorkingPlanQueryHandler
    {
        Task<WorkingPlan> HandleAsync();
    }
}
