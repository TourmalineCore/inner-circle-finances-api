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

    public async Task ExecuteAsync(List<Compensation> compensations)
    {
        _context.UpdateRange(compensations);
        await _context.SaveChangesAsync();
    }
}