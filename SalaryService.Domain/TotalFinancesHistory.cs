using SalaryService.Domain.Common;

namespace SalaryService.Domain
{
    public class TotalFinancesHistory : IIdentityEntity
    {
        public long Id { get; set; }

        public Period Period { get; set; }

        public decimal PayrollExpense { get; set; }

        public decimal TotalExpense { get; set; }
        
    }
}
