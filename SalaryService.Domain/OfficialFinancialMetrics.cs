namespace SalaryService.Domain
{
    public class OfficialFinancialMetrics : FinancialMetrics
    {
        public OfficialFinancialMetrics(FinancesForPayroll financesForPayroll, CoefficientOptions coefficients, WorkingPlan workingPlan) 
            : base(financesForPayroll, coefficients, workingPlan)
        {
        }
    }
}
