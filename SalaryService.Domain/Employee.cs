

namespace SalaryService.Domain
{
    public class Employee
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }

        public EmployeeSalaryPerformance Performances { get; private set; }

        public Employee() { }

        public Employee(long id, string name, string surname, string email, string phone, string skype, string telegram)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
        }
    }
}
