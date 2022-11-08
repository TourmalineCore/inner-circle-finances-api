

namespace SalaryService.Domain
{
    public class Employee
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }

        public Employee(long id, string name, string surname, string email, double ratePerHour, double fullSalary, double employmentType)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            RatePerHour = ratePerHour;
            FullSalary = fullSalary;
            EmploymentType = employmentType;
        }
    }
}
