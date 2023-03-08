using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Dtos
{
    public class EmployeeUpdateParameters
    {
        public long EmployeeId { get; set; }
        public string Phone { get; set; }
        public string PersonalEmail { get; set; }
        public string GitHub { get; set; }
        public string GitLab { get; set; }
        public decimal RatePerHour { get; set; }
        public decimal FullSalary { get; set; }
        public decimal EmploymentType { get; set; }
        public decimal Parking { get; set; }
        public Instant? HireDate { get; set; }
        public bool IsEmployedOfficially { get; set; }
        public string PersonnelNumber { get; set; }

        public EmployeeUpdateParameters(long employeeId, 
            string phone,
            string personalEmail,
            string gitHub,
            string gitLab, 
            decimal ratePerHour,
            decimal fullSalary, 
            decimal employmentType, 
            decimal parking, 
            Instant? hireDate, 
            bool isEmployedOfficially, 
            string personnelNumber)
        {
            EmployeeId = employeeId;
            Phone = phone;
            PersonalEmail = personalEmail;
            GitHub = gitHub;
            GitLab = gitLab;
            RatePerHour = ratePerHour;
            FullSalary = fullSalary;
            EmploymentType = employmentType;
            Parking = parking;
            HireDate = hireDate;
            IsEmployedOfficially = isEmployedOfficially;
            PersonnelNumber = personnelNumber;
        }
    }
}
