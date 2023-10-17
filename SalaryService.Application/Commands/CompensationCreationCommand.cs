using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class CompensationCreationCommand
{
    private readonly EmployeeDbContext _context;

    public CompensationCreationCommand(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task ExecuteAsync(CompensationCreateDto dto)
    {
        await _context.AddRangeAsync(dto.Compensations.Select(x => new Compensation(x.Type, x.Comment, x.Amount, DateOnly.FromDateTime(dto.Date))));
        await _context.SaveChangesAsync();
    }
}
