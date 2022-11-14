using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using static NodaTime.TimeZones.TzdbZone1970Location;

namespace SalaryService.Domain
{
    
    public class MetricsPeriod : BaseValueObject
    {
        public Instant StartedAtUtc { get; set; }
        public Instant? UpdatedAtUtc { get; set; }

        public MetricsPeriod(Instant startedAtUtc, Instant? updatedAtUtc)
        {
            StartedAtUtc = startedAtUtc;
            UpdatedAtUtc = updatedAtUtc;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return StartedAtUtc;
            yield return UpdatedAtUtc;
        }
    }
    public class EmployeeFinancialMetricsHistory
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public MetricsPeriod MetricsPeriod { get; set; }
        public double Salary { get; set; }

        public double HourlyCostFact { get; set; }

        public double HourlyCostHand { get; set; }

        public double Earnings { get; set; }

        public double Expenses { get; set; }

        public double Profit { get; set; }

        public double ProfitAbility { get; set; }

        public double GrossSalary { get; set; }

        public double NetSalary { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public double Retainer { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }

        public double ParkingCostPerMonth { get; set; }

        public double AccountingPerMonth { get; set; }

        public EmployeeFinancialMetricsHistory()
        {
            
        }
    }
}
