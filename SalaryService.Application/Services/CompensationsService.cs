using FluentValidation;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Application.Transactions;
using SalaryService.Application.Validators;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class CompensationsService
{
    private readonly CompensationCreationCommand _compensationCreationCommand;
    //private readonly EmployeeQuery _employeeQuery;
    //private readonly IEmployeesQuery _employeesQuery;

    public CompensationsService(CompensationCreationCommand createCompensationCommandHandler)
    {
        _compensationCreationCommand = createCompensationCommandHandler;
    }

    //public async Task<IEnumerable<Employee>> GetAllAsync()
    //{
    //    return await _employeesQuery.GetEmployeesAsync();
    //}

    public async Task CreateAsync(CompensationCreateDto dto)
    {
        await _compensationCreationCommand.ExecuteAsync(dto);
    }
}
