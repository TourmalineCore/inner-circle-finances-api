namespace SalaryService.Application.Dtos
{
    public class BasicSalaryParametersDto
    {
        public long EmployeeId { get; private set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }

        public BasicSalaryParametersDto(long id, double ratePerHour, double pay, double employmentType, bool hasParking)
        {
            EmployeeId = id;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            HasParking = hasParking;
        }
    }
}
