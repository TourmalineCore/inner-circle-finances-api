namespace SalaryService.Domain
{
    public class EmployeeFinanceForPayroll
    {
        public long Id { get; private set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public double EmploymentType { 
            get => employmentType; 
            private set
            {
                if (!_availableEmploymentRateTypes.Contains(value))
                {
                    throw new ArgumentException("Employment rate type can accept only the following values: 0.5, 1");
                }

                employmentType = value;
            }
        }
        private double employmentType;
        private readonly List<double> _availableEmploymentRateTypes = new() { 0.5, 1 };

        public double ParkingCostPerMonth { get; private set; }
        
        public EmployeeFinanceForPayroll(double ratePerHour, double pay, double employmentType, double parkingCostPerMonth)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
        }

        public void Update(double ratePerHour, double pay, double employmentType, double parkingCostPerMonth)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
        }
    }
}
