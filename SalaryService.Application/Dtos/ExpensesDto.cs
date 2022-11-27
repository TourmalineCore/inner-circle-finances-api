
namespace SalaryService.Application.Dtos
{
    public class ExpensesDto
    {
        public double PayrollExpense { get; private set; }
        public double OfficeExpense { get; private set; }
        public double TotalExpense { get; private set; } 

        public ExpensesDto(double payrollExpense, double officeExpense, double totalExpense)
        {
            PayrollExpense = payrollExpense;
            OfficeExpense = officeExpense;
            TotalExpense = totalExpense;
        }
    }
}
