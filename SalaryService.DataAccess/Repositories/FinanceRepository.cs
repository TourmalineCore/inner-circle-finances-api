using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class FinanceRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public FinanceRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public Task UpdateAsync(EmployeeFinanceForPayroll employeeFinanceForPayroll,
            EmployeeFinancialMetrics metrics,
            EmployeeFinancialMetricsHistory employeeFinancialMetricsHistory)
        {
            using (var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    _employeeDbContext.Update(employeeFinanceForPayroll);
                    _employeeDbContext.Update(metrics);
                    _employeeDbContext.Add(employeeFinancialMetricsHistory);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                }
            }
           
            return _employeeDbContext.SaveChangesAsync();
        }
    }
}
