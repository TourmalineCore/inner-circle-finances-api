using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEstimatedFinancialEfficiencyCommand
    {
    }
    
    public class CreateEstimatedFinancialEfficiencyCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public CreateEstimatedFinancialEfficiencyCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public void Handle(EstimatedFinancialEfficiency estimatedFinancialEfficiency)
        {
            var lastEstimatedFinancialEfficiency = _employeeDbContext.Queryable<EstimatedFinancialEfficiency>().SingleOrDefaultAsync().Result;

            if (lastEstimatedFinancialEfficiency == null)
            {
                _employeeDbContext.Add(estimatedFinancialEfficiency);
            }
            else
            {
                lastEstimatedFinancialEfficiency.Update(estimatedFinancialEfficiency.DesiredEarnings,
                    estimatedFinancialEfficiency.DesiredProfit,
                    estimatedFinancialEfficiency.DesiredProfitability,
                    estimatedFinancialEfficiency.ReserveForQuarter,
                    estimatedFinancialEfficiency.ReserveForHalfYear,
                    estimatedFinancialEfficiency.ReserveForYear);

                _employeeDbContext.Update(lastEstimatedFinancialEfficiency);
            }

            _employeeDbContext.SaveChangesAsync();
        }
    }
}
