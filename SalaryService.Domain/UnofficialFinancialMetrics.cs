namespace SalaryService.Domain
{
    public class UnofficialFinancialMetrics : FinancialMetrics
    {
        public UnofficialFinancialMetrics(FinancesForPayroll financesForPayroll, CoefficientOptions coefficients, WorkingPlan workingPlan) 
            : base(financesForPayroll, coefficients, workingPlan)
        {
        }

        protected override decimal CalculateDistrictCoefficient() => 0;

        protected override decimal CalculatePrepayment() => 0;

        protected override decimal GetNdflValue() => 0;

        protected override decimal GetPensionContributions() => 0;

        protected override decimal GetMedicalContributions() => 0;

        protected override decimal GetSocialInsuranceContributions() => 0;

        protected override decimal GetInjuriesContributions() => 0;

        protected override decimal CalculateGrossSalary() => 0;

        protected override decimal CalculateNetSalary() => 0;
    }
}
