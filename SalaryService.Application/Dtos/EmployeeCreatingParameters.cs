using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public partial class EmployeeCreatingParameters
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string CorporateEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string GitHub { get; set; }

        public string GitLab { get; set; }

        public decimal RatePerHour { get; set; }

        public decimal Pay { get; set; }

        public decimal EmploymentType { get; set; }

        public decimal ParkingCostPerMonth { get; set; }
    }
}
