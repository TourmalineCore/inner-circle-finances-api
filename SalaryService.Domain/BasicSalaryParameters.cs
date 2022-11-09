

namespace SalaryService.Domain
{
    public class BasicSalaryParameters
    {
        public long Id { get; private set; }
        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public double EmploymentType { get; private set; }

        public bool HasParking { get; private set; }
        public long EmployeeId { get; private set; }

        public Employee Employee { get; private set; }

        private BasicSalaryParameters() { }
        public BasicSalaryParameters(long id, long employeeId, double ratePerHour, double pay, double employmentType, bool hasParking)
        {
            Id = id;
            EmployeeId = employeeId;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            HasParking = hasParking;
        }
    }
}
