namespace SalaryService.Application.Dtos
{
    public class BasicSalaryParametersDto
    {
        public long Id { get; private set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public double EmploymentType { get; set; }

        public BasicSalaryParametersDto(long id, double ratePerHour, double pay, double employmentType)
        {
            Id = id;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
        }
    }
}
