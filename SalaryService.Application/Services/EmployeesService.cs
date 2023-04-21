using FluentValidation;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Application.Transactions;
using SalaryService.Application.Validators;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class EmployeesService
{
    private readonly EmployeeCreationCommand _employeeCreationCommand;
    private readonly EmployeeQuery _employeeQuery;
    private readonly IEmployeesQuery _employeesQuery;
    private readonly ProfileUpdateCommand _employeeProfileUpdateCommand;
    private readonly EmployeeUpdateParametersValidator _employeeUpdateParametersValidator;
    private readonly EmployeeDismissalTransaction _employeeDismissalTransaction;
    private readonly EmployeeUpdateTransaction _employeeUpdateTransaction;

    public EmployeesService(
        EmployeeCreationCommand createEmployeeCommandHandler,
        ProfileUpdateCommand updateProfileCommandHandler,
        EmployeeUpdateParametersValidator employeeUpdateParametersValidator,
        EmployeeUpdateTransaction employeeUpdateTransaction,
        EmployeeDismissalTransaction employeeDismissalTransaction,
        EmployeeQuery employeeQuery,
        IEmployeesQuery employeesQuery)
    {
        _employeeCreationCommand = createEmployeeCommandHandler;
        _employeeProfileUpdateCommand = updateProfileCommandHandler;
        _employeeUpdateParametersValidator = employeeUpdateParametersValidator;
        _employeeUpdateTransaction = employeeUpdateTransaction;
        _employeeDismissalTransaction = employeeDismissalTransaction;
        _employeeQuery = employeeQuery;
        _employeesQuery = employeesQuery;
    }

    public async Task<Employee> GetByIdAsync(long employeeId)
    {
        return await _employeeQuery.GetEmployeeAsync(employeeId);
    }

    public async Task<Employee> GetByCorporateEmailAsync(string corporateEmail)
    {
        return await _employeeQuery.GetEmployeeAsync(corporateEmail);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(bool includeFinanceInfo)
    {
        return await _employeesQuery.GetEmployeesAsync(includeFinanceInfo);
    }

    public async Task CreateAsync(EmployeeCreationParameters parameters)
    {
        await _employeeCreationCommand.ExecuteAsync(parameters);
    }

    public async Task UpdateAsync(EmployeeUpdateDto request)
    {
        var validationResult = await _employeeUpdateParametersValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors[0].ErrorMessage);
        }

        await _employeeUpdateTransaction.ExecuteAsync(request);
    }

    public async Task UpdateProfileAsync(ProfileUpdatingParameters updatingParameters)
    {
        await _employeeProfileUpdateCommand.ExecuteAsync(updatingParameters);
    }

    public async Task DismissAsync(long id)
    {
        await _employeeDismissalTransaction.ExecuteAsync(id);
    }
}
