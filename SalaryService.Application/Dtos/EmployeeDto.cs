namespace SalaryService.Application.Dtos
{
    public class EmployeeDto
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }

        public EmployeeDto(long id, string name, string surname, string email, string phone, string skype, string telegram)
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
