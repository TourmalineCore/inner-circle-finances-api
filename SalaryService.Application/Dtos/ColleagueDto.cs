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

        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public EmploymentTypes EmploymentType { get; private set; }

        public double NetSalary { get; private set; }

        public double Parking { get; private set; }

        public ColleagueFinancesDto(long id, string fullName, double ratePerHour, double pay, EmploymentTypes employmentType, double netSalary, double parking)
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
    public class ColleagueDto
    {
        public IEnumerable<ColleagueContactsDto> ColleagueContacts { get; private set; }
        public IEnumerable<ColleagueFinancesDto> ColleagueFinancesDto { get; private set; }

        public ColleagueDto(IEnumerable<ColleagueContactsDto> colleagueContacts, IEnumerable<ColleagueFinancesDto> colleagueFinancesDto)
        {
            ColleagueContacts = colleagueContacts;
            ColleagueFinancesDto = colleagueFinancesDto;
        }
    }
}
