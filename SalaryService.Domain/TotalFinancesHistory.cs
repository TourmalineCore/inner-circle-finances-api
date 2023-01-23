using SalaryService.Domain.Common;

namespace SalaryService.Domain
{
    public class TotalFinancesHistory : IIdentityEntity
    {
        public long Id { get; set; }
        public Period Period { get; set; }
        public double PayrollExpense { get; set; }
        public double TotalExpense { get; set; }
        
    }
}
