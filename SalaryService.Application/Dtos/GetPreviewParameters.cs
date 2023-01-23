using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class GetPreviewParameters
    {
        public double RatePerHour { get; set; }
        public double Pay { get; set; }
        public double EmploymentTypeValue { get; set; }
        public double ParkingCostPerMonth { get; set; }
    }
}
