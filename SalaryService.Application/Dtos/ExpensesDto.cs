
namespace SalaryService.Application.Dtos
{
    public class ExpensesDto
    {
        public decimal PayrollExpense { get; private set; }
        public decimal OfficeExpense { get; private set; }
        public decimal TotalExpense { get; private set; } 

        public ExpensesDto(decimal payrollExpense, decimal officeExpense, decimal totalExpense)
        {
            PayrollExpense = payrollExpense;
            OfficeExpense = officeExpense;
            TotalExpense = totalExpense;
        }
    }
}
