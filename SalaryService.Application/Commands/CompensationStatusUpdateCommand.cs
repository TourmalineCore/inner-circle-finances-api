using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class CompensationStatusUpdateCommand
{
    private readonly EmployeeDbContext _context;

    public CompensationStatusUpdateCommand(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task ExecuteAsync(Compensation compensation)
    {
        _context.Update(compensation);
        await _context.SaveChangesAsync();
    }
}