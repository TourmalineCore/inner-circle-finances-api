using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class ColleagueContactsDto
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public string CorporateEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string GitHub { get; private set; }

        public string GitLab { get; private set; }

        public ColleagueContactsDto(long id, string fullName, string corporateEmail, string personalEmail, string phone, string gitHub, string gitLab)
        {
            Id = id;
            FullName = fullName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
        }
    }

    public class ColleagueFinancesDto
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public decimal RatePerHour { get; private set; }

        public decimal Pay { get; private set; }

        public decimal EmploymentType { get; private set; }

        public decimal NetSalary { get; private set; }

        public decimal Parking { get; private set; }

        public ColleagueFinancesDto(long id, string fullName, decimal ratePerHour, decimal pay, decimal employmentType, decimal netSalary, decimal parking)
        {
            Id = id;
            FullName = fullName;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            NetSalary = netSalary;
            Parking = parking;
        }
    }

    public readonly struct EmployeeDto
    {
        public long EmployeeId { get; init; }

        public string FullName { get; init; }

        public string CorporateEmail { get; init; }

        public string? PersonalEmail { get; init; }

        public string? Phone { get; init; }

        public string? GitHub { get; init; }

        public string? GitLab { get; init; }

        public bool IsBlankEmployee { get; init; }

        public bool IsCurrentEmployee { get; init; }

        public decimal? NetSalary { get; init; } = null;

        public decimal? RatePerHour { get; init; } = null;

        public decimal? FullSalary { get; init; } = null;

        public decimal? EmploymentType { get; init; } = null;

        public decimal? Parking { get; init; } = null;

        public string? PersonnelNumber { get; init; }

        public DateTime? HireDate { get; init; }

        public EmployeeDto(Employee employee)
        {
            EmployeeId = employee.Id;
            FullName = $"{employee.FirstName} {employee.LastName} {employee.MiddleName}";
            CorporateEmail = employee.CorporateEmail;
            PersonalEmail = employee.PersonalEmail;
            Phone = employee.Phone;
            GitHub = employee.GitHub;
            GitLab = employee.GitLab;
            IsBlankEmployee = employee.IsBlankEmployee;
            IsCurrentEmployee = employee.IsCurrentEmployee;

            if (employee.EmployeeFinancialMetrics != null)
            {
                NetSalary = employee.EmployeeFinancialMetrics.NetSalary;
                RatePerHour = employee.EmployeeFinancialMetrics.RatePerHour;
                FullSalary = employee.EmployeeFinancialMetrics.Pay;
                Parking = employee.EmployeeFinancialMetrics.ParkingCostPerMonth;
                EmploymentType = employee.EmployeeFinancialMetrics.EmploymentType;
            }

            PersonnelNumber = "01/20"; // implement set up when will do task #861mc17vg
            HireDate = DateTime.UtcNow; // implement set up when will do task #861mc17vg
        }
    }
}
