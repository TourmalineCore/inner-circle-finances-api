using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetTotalFinancesQuery
    {
    }

    public class GetTotalFinancesQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetTotalFinancesQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<TotalFinancesDto> Handle()
        {
            var totals = await _employeeDbContext.Set<TotalFinances>().SingleAsync();
            var coefficients = await _employeeDbContext.Set<CoefficientOptions>().SingleAsync();
            return new TotalFinancesDto(new ExpensesDto(totals.PayrollExpense, coefficients.OfficeExpenses, totals.TotalExpense));
        }
    }
}
