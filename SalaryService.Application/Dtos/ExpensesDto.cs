
namespace SalaryService.Application.Dtos
{
    public class ExpensesDto
    {
        public double PayrollExpense { get; private set; }
        public double OfficeExpense { get; private set; }
        public double TotalExpense { get; private set; } 

        public ExpensesDto(double payrollExpense, double officeExpenses)
        {
            PayrollExpense = payrollExpense;
            OfficeExpense = officeExpenses;
            TotalExpense = PayrollExpense + OfficeExpense;
        }
    }
}
