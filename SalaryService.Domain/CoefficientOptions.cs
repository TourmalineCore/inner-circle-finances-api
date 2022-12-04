namespace SalaryService.Domain
{
    public class CoefficientOptions : IIdentityEntity
    {
        public long Id { get; set; }

        public double DistrictCoefficient { get; set; }

        public double MinimumWage { get; set; }

        public double IncomeTaxPercent { get; set; }

        public double OfficeExpenses { get; set; }

        public CoefficientOptions(long id, double districtCoefficient, double minimumWage, double incomeTaxPercent, double officeExpenses)
        {
            Id = id;
            DistrictCoefficient = districtCoefficient;
            MinimumWage = minimumWage;
            IncomeTaxPercent = incomeTaxPercent;
            OfficeExpenses = officeExpenses;
        }
    }
}
