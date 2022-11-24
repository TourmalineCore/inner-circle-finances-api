using NodaTime;

namespace SalaryService.Domain
{
    public class Employee : IIdentityEntity
    {
        public long Id { get; private set; }

        public long AccountId { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string MiddleName { get; private set; }

        public string CorporateEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string? Phone { get; private set; }

        public string? GitHub { get; private set; }

        public string? GitLab { get; private set; }

        public long FinanceForPayrollId { get; private set; }
        public EmployeeFinanceForPayroll EmployeeFinanceForPayroll { get; private set; }

        public long FinancialMetricsId { get; private set; }
        public EmployeeFinancialMetrics EmployeeFinancialMetrics { get; private set; }

        public Instant HireDate { get; private set; }

        public Instant? DeletedAtUtc { get; private set; } = null;

        public Employee(string name, 
            string surname, 
            string middleName, 
            string corporateEmail, 
            string personalEmail,
            string phone,
            string gitHub, 
            string gitLab,
            Instant hireDate)
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
            HireDate = hireDate;
            
        }

        public void AddMetricsAndFinanceForpayroll(long financeForPayrollId,
            long financialMetricsId)
        {
            FinanceForPayrollId = financeForPayrollId;
            FinancialMetricsId = financialMetricsId;
        }

        public void Delete(Instant deletedAtUtc)
        {
            DeletedAtUtc = deletedAtUtc;
        }

        public void Update(string name, 
            string surname, 
            string middleName, 
            string workEmail, 
            string personalEmail,
            string phone,
            string gitHub,
            string gitLab)
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            CorporateEmail = workEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            GitHub = gitHub;
            GitLab = gitLab;
        }
    }
}
