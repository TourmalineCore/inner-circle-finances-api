namespace SalaryService.Application.Dtos
{
    public class CEODto
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public string CorporateEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string GitHub { get; private set; }

        public string GitLab { get; private set; }

        public CEODto(long id, 
            string name, 
            string surname, 
            string middleName, 
            string corporateEmail, 
            string personalEmail, 
            string phone,
            string gitHub,
            string gitLab)
        {
            Id = id;
            FullName = name + " " + surname + " " + middleName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
        }
    }
}
