namespace SalaryService.Application.Dtos
{
    public readonly struct EmployeeCreationParameters
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string MiddleName { get; init; }

        public string CorporateEmail { get; init; }
    }
}
