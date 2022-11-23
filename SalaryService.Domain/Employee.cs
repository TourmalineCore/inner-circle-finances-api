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

        public string? PersonalEmail { get; private set; }

        public string? Phone { get; private set; }

        public string? Skype { get; private set; }

        public string? Telegram { get; private set; }

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
            string skype, 
            string telegram, 
            Instant hireDate)
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            CorporateEmail = corporateEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
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
            string skype, 
            string telegram)
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            CorporateEmail = workEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
        }
    }
}
