
using System.Xml.Linq;
using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class EmployeeDto
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public string CorporateEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string GitHub { get; private set; }

        public string GitLab { get; private set; }

        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public EmploymentTypes EmploymentType { get; private set; }

        public double NetSalary { get; private set; }

        public double Parking { get; private set; }

        public EmployeeDto(long id, 
            string name,
            string surname, 
            string middleName, 
            string corporateEmail, 
            string personalEmail, 
            string phone, 
            string gitHub, 
            string gitLab, 
            double ratePerHour, 
            double pay,
            EmploymentTypes employmentType, 
            double netSalary, 
            double parking)
        {
            Id = id;
            FullName = name + " " + surname + " " + middleName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            NetSalary = netSalary;
            Parking = parking;
        }
    }
}
