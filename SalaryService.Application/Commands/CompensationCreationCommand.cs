using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;
using SalaryService.Application.Queries;

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

        var types = CompensationTypes.GetTypeList().Select(x => x.Name.ToLower());

        var dateCompensation = dto.DateCompensation;

        var compensations = dto.Compensations.Select(x => new Compensation(x.Type, x.Comment, x.Amount, employee, dateCompensation, x.IsPaid));
        foreach (Compensation c in compensations)
        {
            if (types.Contains(c.Type))
            {
                await _context.AddRangeAsync(compensations);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Type [{c.Type}] doesn't exists");
            }
        }
    }
}
