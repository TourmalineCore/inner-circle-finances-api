using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class EmployeeCreationCommand
{
    private readonly EmployeeDbContext _context;

    public EmployeeCreationCommand(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task ExecuteAsync(EmployeeCreationParameters parameters)
    {
        var employee = new Employee(
            parameters.FirstName,
            parameters.LastName,
            parameters.MiddleName,
            parameters.CorporateEmail,
            parameters.TenantId
        );

        await _context.AddAsync(employee);
        await _context.SaveChangesAsync();
    }
}
