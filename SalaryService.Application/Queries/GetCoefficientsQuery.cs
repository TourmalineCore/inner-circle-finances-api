using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetCoefficientsQuery
    {
    }
    
    public class GetCoefficientsQueryHandler : IGetCoefficientsQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetCoefficientsQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<CoefficientOptions> HandleAsync()
        {
            return await _employeeDbContext.Queryable<CoefficientOptions>().SingleAsync();
        }
    }
}
