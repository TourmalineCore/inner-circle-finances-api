namespace SalaryService.Domain
{
    public class EmployeeFinanceForPayroll
    {
        public long Id { get; private set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public decimal RatePerHour { get; private set; }

        public decimal Pay { get; private set; }

        public decimal ParkingCostPerMonth { get; private set; }

        public decimal EmploymentType { get; private set; }

        public EmployeeFinanceForPayroll(decimal ratePerHour, decimal pay, decimal employmentType, decimal parkingCostPerMonth, long? employeeId = null)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;

            if (employeeId.HasValue)
            {
                EmployeeId = employeeId.Value;
            }
        }

        public EmployeeFinanceForPayroll()
        {
        }

        public void Update(decimal ratePerHour, decimal pay, decimal employmentType, decimal parkingCostPerMonth)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
        }
    }
}
