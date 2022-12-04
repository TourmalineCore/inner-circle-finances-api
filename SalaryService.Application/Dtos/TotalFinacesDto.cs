namespace SalaryService.Application.Dtos
{
    public class TotalFinancesDto
    {
        public ExpensesDto TotalExpenses { get; private set; }

        public TotalFinancesDto(ExpensesDto totalExpenses)
        {
            TotalExpenses = totalExpenses;
        }
    }
}
