namespace SalaryService.Domain
{
    public class EmployeeFinanceForPayroll
    {
        public long Id { get; private set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public double RatePerHour { get; private set; }

        public double Pay { get; private set; }

        public double EmploymentType { get; private set; }

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
