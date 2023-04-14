using SalaryService.Domain;

namespace SalaryService.Application.Queries.Contracts
{
    public interface IGetCoefficientsQueryHandler
    {
        Task<CoefficientOptions> HandleAsync();
    }
}
