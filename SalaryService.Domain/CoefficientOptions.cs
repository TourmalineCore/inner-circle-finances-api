namespace SalaryService.Domain;

public class CoefficientOptions : IIdentityEntity
{
    public long Id { get; set; }

    public decimal DistrictCoefficient { get; set; }

    public decimal MinimumWage { get; set; }

    public decimal IncomeTaxPercent { get; set; }

    public decimal OfficeExpenses { get; set; }

    public CoefficientOptions(long id, decimal districtCoefficient, decimal minimumWage, decimal incomeTaxPercent, decimal officeExpenses)
    {
        Id = id;
        DistrictCoefficient = districtCoefficient;
        MinimumWage = minimumWage;
        IncomeTaxPercent = incomeTaxPercent;
        OfficeExpenses = officeExpenses;
    }
}
