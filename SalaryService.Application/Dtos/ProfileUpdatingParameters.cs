namespace SalaryService.Application.Dtos
{
    public class ProfileUpdatingParameters
    {
        public long AccountId { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string GitHub { get; set; }

        public string GitLab { get; set; }
    }
}
