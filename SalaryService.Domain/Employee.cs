

namespace SalaryService.Domain
{
    public class Employee
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string WorkEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }

        public EmployeeSalaryPerformance Performances { get; private set; }

        public Employee() { }

        public Employee(long id, string name, string surname, string workEmail, string personalEmail, string phone, string skype, string telegram)
        {
            Id = id;
            Name = name;
            Surname = surname;
            WorkEmail = workEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
        }
    }
}
