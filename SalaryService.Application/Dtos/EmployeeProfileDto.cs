using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public readonly struct EmployeeProfileDto
    {
        public long Id { get; init; }

        public string FullName { get; init; }

        public string CorporateEmail { get; init; }

        public string? PersonalEmail { get; init; }

        public string? Phone { get; init; }

        public string? GitHub { get; init; }

        public string? GitLab { get; init; }

        public decimal? FullSalary { get; init; } = null;

        public decimal? DistrictCoefficient { get; init; } = null;

        public decimal? IncomeTax { get; init; } = null;

        public decimal? NetSalary { get; init; } = null;

        public EmployeeProfileDto(Employee employee)
        {
            Id = employee.Id;
            FullName = employee.GetFullName();
            CorporateEmail = employee.CorporateEmail;
            PersonalEmail = employee.PersonalEmail;
            Phone = employee.Phone;
            GitHub = employee.GitHub;
            GitLab = employee.GitLab;

            if (employee.EmployeeFinancialMetrics != null)
            {
                FullSalary = Math.Round(employee.EmployeeFinancialMetrics.Salary, 2);
                DistrictCoefficient = Math.Round(employee.EmployeeFinancialMetrics.DistrictCoefficient, 2);
                IncomeTax = Math.Round(employee.EmployeeFinancialMetrics.IncomeTaxContributions, 2);
                NetSalary = Math.Round(employee.EmployeeFinancialMetrics.NetSalary, 2);
            }
        }
    }
}