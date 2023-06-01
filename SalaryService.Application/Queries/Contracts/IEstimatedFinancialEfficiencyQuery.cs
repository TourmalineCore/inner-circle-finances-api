using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface IEstimatedFinancialEfficiencyQuery
{
    Task<EstimatedFinancialEfficiency?> GetEstimatedFinancialEfficiencyAsync();
}