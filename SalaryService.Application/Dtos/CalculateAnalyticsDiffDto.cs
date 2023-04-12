namespace SalaryService.Application.Dtos
{
    public readonly struct CalculateAnalyticsDiffDto
    {
        public IEnumerable<EmployeeRowDto> Employees { get; init; }
    }
}
