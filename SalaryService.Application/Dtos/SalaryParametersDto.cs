namespace SalaryService.Application.Dtos
{
    public class SalaryParametersDto
    {
        public long Id { get; private set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }

        public SalaryParametersDto(long id, double ratePerHour, double fullSalary, double employmentType)
        {
            Id = id;
            RatePerHour = ratePerHour;
            FullSalary = fullSalary;
            EmploymentType = employmentType;
        }
    }
}
