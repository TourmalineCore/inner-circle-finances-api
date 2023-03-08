using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Dtos
{
    public class CollegueInfoDto
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public string CorporateEmail { get; set; }
        public string? PersonalEmail { get; set; }
        public string? Phone { get; set; }
        public string? GitHub { get; set; }
        public string? GitLab { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? RatePerHour { get; set; }
        public decimal? FullSalary { get; set; }
        public decimal? EmploymentType { get; set; }
        public decimal? Parking { get; set; }
        public string? PersonnelNumber { get; set; }
        public Instant? HireDate { get; set; }

        public CollegueInfoDto(long employeeId,
            string fullName,
            string corporateEmail,
            string personalEmail,
            string phone,
            string gitHub,
            string gitLab)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
        }
        public CollegueInfoDto(long employeeId,
            string fullName,
            string corporateEmail,
            string personalEmail,
            string phone,
            string gitHub,
            string gitLab,
            decimal netSalary,
            decimal ratePerHour,
            decimal fullSalary,
            decimal employmentType,
            decimal parking,
            string personnelNumber,
            Instant? hireDate)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
            NetSalary = netSalary;
            RatePerHour = ratePerHour;
            FullSalary = fullSalary;
            EmploymentType = employmentType;
            Parking = parking;
            PersonnelNumber = personnelNumber;
            HireDate = hireDate;
        }
    }
}
