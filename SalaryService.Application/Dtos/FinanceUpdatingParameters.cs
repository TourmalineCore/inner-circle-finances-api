using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class FinanceUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public decimal RatePerHour { get; set; }

        public decimal Pay { get; set; }

        public decimal EmploymentType { get; set; }

        public decimal ParkingCostPerMonth { get; set; }
    }
}
