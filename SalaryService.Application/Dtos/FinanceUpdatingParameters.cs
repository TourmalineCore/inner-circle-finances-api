using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class FinanceUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public double EmploymentTypeValue => EmploymentType == EmploymentTypes.FullTime ? 1.0 : 0.5;

        public double ParkingCostPerMonth { get; set; }
    }
}
