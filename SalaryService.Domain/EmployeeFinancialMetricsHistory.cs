

namespace SalaryService.Domain
{
    public class EmployeeFinancialMetricsHistory : AbstractMetricsEntity
    {
        public DateTime ChangedAt { get; private set; }
       
        public EmployeeFinancialMetricsHistory(long employeeId, double ratePerHour, double pay, double employmentType, bool hasParking) : base(employeeId, ratePerHour, pay, employmentType, hasParking)
        {
            ChangedAt = DateTime.UtcNow.Date;
        }
    }
}
