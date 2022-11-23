namespace SalaryService.Application.Dtos
{
    public class EmployeeDto
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string MiddleName { get; private set; }

        public string CorporateEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }

        public string HireDate { get; private set; }

        public double NetSalary { get; private set; }

        public EmployeeDto(long id, 
            string name, 
            string surname, 
            string middleName, 
            string corporateEmail, 
            string personalEmail, 
            string phone, 
            string skype, 
            string telegram,
            string hireDate,
            double netSalary)
        {
            Id = id;
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
            HireDate = hireDate;
            NetSalary = netSalary;
        }
    }
}
