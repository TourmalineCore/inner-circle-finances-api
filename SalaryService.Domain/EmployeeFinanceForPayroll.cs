namespace SalaryService.Domain
{
    public enum EmploymentTypes
    {
        FullTime,
        PartTime
    }
    public class EmployeeFinanceForPayroll
    {
        public long Id { get; private set; }

        public Employee Employee { get; private set; }

        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public EmploymentTypes EmploymentType { get; private set; }

        public double EmploymentTypeValue { get; private set; }

        public bool HasParking { get; private set; }

        public long EmployeeId { get; private set; }
        

        public EmployeeFinanceForPayroll(long employeeId, double ratePerHour, double pay, EmploymentTypes employmentType, bool hasParking)
        {
            EmployeeId = employeeId;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            switch (employmentType)
            {
                case EmploymentTypes.FullTime:
                    EmploymentTypeValue = 1.0;
                    break;
                case EmploymentTypes.PartTime:
                    EmploymentTypeValue = 0.5;
                    break;
            }
            HasParking = hasParking;
        }

        public void Update(double ratePerHour, double pay, EmploymentTypes employmentType, bool hasParking)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            HasParking = hasParking;
        }
    }
}
