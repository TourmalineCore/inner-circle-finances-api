using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetFinancialMetricsQuery
    {
    }
    
    public partial class GetFinancialMetricsQueryHandler : IGetFinancialMetricsQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetFinancialMetricsQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<EmployeeFinancialMetrics>> HandleAsync()
        {
            return await _employeeDbContext
                .QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
        }
    }
}
