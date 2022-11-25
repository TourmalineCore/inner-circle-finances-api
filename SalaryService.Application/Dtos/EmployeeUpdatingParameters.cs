namespace SalaryService.Application.Dtos
{
    public class EmployeeUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string GitHub { get; set; }

        public string GitLab { get; set; }
    }
}
