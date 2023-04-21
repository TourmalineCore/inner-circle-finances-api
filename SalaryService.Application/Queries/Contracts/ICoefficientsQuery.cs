using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts;

public interface ICoefficientsQuery
{
    Task<CoefficientOptions> GetCoefficientsAsync();
}
