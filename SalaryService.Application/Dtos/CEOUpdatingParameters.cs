namespace SalaryService.Application.Dtos
{
    public partial class CEOUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string GitHub { get; set; }

        public string GitLab { get; set; }
    }
}
