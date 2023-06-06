using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class ProfileUpdateCommand
{
    private readonly EmployeeDbContext _context;

    public ProfileUpdateCommand(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task ExecuteAsync(string corporateEmail, ProfileUpdatingParameters request)
    {
        var employee = await _context
            .Queryable<Employee>()
            .SingleAsync(x => x.CorporateEmail == corporateEmail && x.DeletedAtUtc == null);

        employee.Update(request.PersonalEmail, request.Phone, request.GitHub, request.GitLab);

        _context.Update(employee);
        await _context.SaveChangesAsync();
    }
}