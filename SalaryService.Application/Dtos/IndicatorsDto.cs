namespace SalaryService.Application.Dtos
{
    public class IndicatorsDto
    {
        public ExpensesDto TotalExpenses { get; private set; }

        public DesiredFinancialMetricsDto DesiredFinancialMetrics { get; private set; }

        public ReserveFinanceDto ReserveFinance { get; private set; }

        public WorkingDaysDto WorkingDays { get; private set; }

        public decimal IncomeTaxPercent { get; private set; }

        public decimal DistrictCoefficient { get; private set; }

        public decimal MinimumWage { get; private set; }

        public IndicatorsDto(ExpensesDto totalExpenses, 
            DesiredFinancialMetricsDto desiredFinancialMetrics, 
            ReserveFinanceDto reserveFinance, 
            WorkingDaysDto workingDays,
            decimal incomeTaxPercent,
            decimal districtCoefficient,
            decimal minimumWage)
        {
            TotalExpenses = totalExpenses;
            DesiredFinancialMetrics = desiredFinancialMetrics;
            ReserveFinance = reserveFinance;
            WorkingDays = workingDays;
            IncomeTaxPercent = incomeTaxPercent;
            DistrictCoefficient = districtCoefficient;
            MinimumWage = minimumWage;
        }
    }
}
