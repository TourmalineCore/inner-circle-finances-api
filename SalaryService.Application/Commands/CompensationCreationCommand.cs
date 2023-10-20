using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;

namespace SalaryService.Application.Commands;

public class CompensationCreationCommand
{
    private readonly EmployeeDbContext _context;
    private readonly EmployeeQuery _employeeQuery;

    public CompensationCreationCommand(EmployeeDbContext employeeDbContext, EmployeeQuery employeeQuery)
    {
        _context = employeeDbContext;
        _employeeQuery = employeeQuery;
    }

    public async Task ExecuteAsync(CompensationCreateDto dto)
    {
        var employee = await _employeeQuery.GetEmployeeAsync(dto.EmployeeId);

        await _context.AddRangeAsync(dto.Compensations.Select(x => new Compensation(x.Type, x.Comment, x.Amount, employee, x.Date, x.IsPaid)));
        await _context.SaveChangesAsync();
    }
}
