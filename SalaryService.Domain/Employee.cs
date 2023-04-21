using NodaTime;

namespace SalaryService.Domain;

public class Employee : IIdentityEntity
{
    public long Id { get; set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? MiddleName { get; private set; }

    public string CorporateEmail { get; private set; }

    public string? PersonalEmail { get; private set; }

    public string? Phone { get; private set; }

    public string? GitHub { get; private set; }

    public string? GitLab { get; private set; }

    public FinancialMetrics? FinancialMetrics { get; set; }

    public Instant? HireDate { get; private set; }

    public bool IsBlankEmployee { get; private set; }

    public bool IsCurrentEmployee { get; private set; }

    public bool IsEmployedOfficially { get; private set; }

    public string? PersonnelNumber { get; private set; }

    public Instant? DeletedAtUtc { get; private set; }

    public Employee(string firstName, string lastName, string middleName, string corporateEmail)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        CorporateEmail = corporateEmail;
        IsBlankEmployee = true;
        IsCurrentEmployee = false;
        IsEmployedOfficially = false;
    }

    public void Delete(Instant deletedAtUtc)
    {
        DeletedAtUtc = deletedAtUtc;
    }

    public void Update(
        string personalEmail,
        string phone,
        string gitHub,
        string gitLab)
    {
        PersonalEmail = personalEmail;
        Phone = phone;
        GitHub = gitHub;
        GitLab = gitLab;
    }
    public void Update(
        string phone,
        string? personalEmail,
        string? gitHub,
        string? gitLab,
        Instant? hireDate,
        bool isEmployedOfficially,
        string? personnelNumber)
    {
        Phone = phone;
        PersonalEmail = personalEmail;
        GitHub = gitHub;
        GitLab = gitLab;
        HireDate = hireDate;
        IsEmployedOfficially = isEmployedOfficially;
        IsBlankEmployee = false;
        IsCurrentEmployee = true;
        PersonnelNumber = personnelNumber;
    }

    public void UpdateFinancialMetrics(FinancesForPayroll financesForPayroll, CoefficientOptions coefficients, WorkingPlan workingPlan, Instant createdAtUtc)
    {
        FinancialMetrics = IsEmployedOfficially
            ? new FinancialMetrics(financesForPayroll, coefficients, workingPlan, createdAtUtc)
            : new UnofficialFinancialMetrics(financesForPayroll, coefficients, workingPlan, createdAtUtc);
    }

    public string GetFullName()
    {
        return MiddleName == null 
            ? $"{LastName} {FirstName} " 
            : $"{LastName} {FirstName} {MiddleName}";
    }
}
