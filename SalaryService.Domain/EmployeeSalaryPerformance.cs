
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryService.Domain
{
    public class EmployeeSalaryPerformance
    {
        public long Id { get; private set; }
        public double RatePerHour { get; private set; }

        public double FullSalary { get; private set; }

        public double EmploymentType { get; private set; }

        public bool HasParking { get; private set; }
        public long EmployeeId { get; private set; }

        public Employee Employee { get; private set; }

        private EmployeeSalaryPerformance() { }
        public EmployeeSalaryPerformance(long id, long employeeId, double ratePerHour, double fullSalary, double employmentType, bool hasParking)
        {
            Id = id;
            EmployeeId = employeeId;
            RatePerHour = ratePerHour;
            FullSalary = fullSalary;
            EmploymentType = employmentType;
            HasParking = hasParking;
        }
    }
}
