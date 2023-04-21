namespace SalaryService.Domain;

public class FinancesForPayroll
{
    public decimal RatePerHour { get; init; }

    public decimal Pay { get; init; }

    public decimal ParkingCostPerMonth { get; init; }

    public decimal EmploymentType { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public FinancesForPayroll(decimal ratePerHour, 
        decimal pay,
        decimal parkingCostPerMonth,
        decimal employmentType,
        bool isEmployedOfficially)
    {
        RatePerHour = ratePerHour;
        Pay = pay;
        EmploymentType = employmentType;
        ParkingCostPerMonth = parkingCostPerMonth;
        IsEmployedOfficially = isEmployedOfficially;
    }
}
