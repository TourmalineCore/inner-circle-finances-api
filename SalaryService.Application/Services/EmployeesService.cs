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
    private readonly ProfileUpdateCommand _profileUpdateCommand;
    private readonly EmployeeUpdateParametersValidator _employeeUpdateParametersValidator;
    private readonly EmployeeDismissalTransaction _employeeDismissalTransaction;
    private readonly EmployeeUpdateTransaction _employeeUpdateTransaction;
    private readonly EmployeesForAnalyticsQuery _employeesForAnalyticsQuery;
    private readonly CurrentEmployeesQuery _currentEmployeesQuery;

    public EmployeesService(
        EmployeeCreationCommand createEmployeeCommandHandler,
        ProfileUpdateCommand updateProfileCommandHandler,
        EmployeeUpdateParametersValidator employeeUpdateParametersValidator,
        EmployeeUpdateTransaction employeeUpdateTransaction,
        EmployeeDismissalTransaction employeeDismissalTransaction,
        EmployeeQuery employeeQuery,
        IEmployeesQuery employeesQuery, 
        EmployeesForAnalyticsQuery employeesForAnalyticsQuery, 
        CurrentEmployeesQuery currentEmployeesQuery)
    {
        _employeeCreationCommand = createEmployeeCommandHandler;
        _profileUpdateCommand = updateProfileCommandHandler;
        _employeeUpdateParametersValidator = employeeUpdateParametersValidator;
        _employeeUpdateTransaction = employeeUpdateTransaction;
        _employeeDismissalTransaction = employeeDismissalTransaction;
        _employeeQuery = employeeQuery;
        _employeesQuery = employeesQuery;
        _employeesForAnalyticsQuery = employeesForAnalyticsQuery;
        _currentEmployeesQuery = currentEmployeesQuery;
    }

    public async Task<Employee> GetByIdAsync(long employeeId)
    {
        return await _employeeQuery.GetEmployeeAsync(employeeId);
    }

    public async Task<Employee> GetByCorporateEmailAsync(string corporateEmail)
    {
        return await _employeeQuery.GetEmployeeAsync(corporateEmail);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _employeesQuery.GetEmployeesAsync();
    }

    public async Task<IEnumerable<Employee>> GetCurrentEmployeesAsync()
    {
        return await _currentEmployeesQuery.GetCurrentEmployeesAsync();
    }

    public async Task<IEnumerable<Employee>> GetEmployeesForAnalytics()
    {
        return await _employeesForAnalyticsQuery.GetEmployeesForAnalyticsAsync();
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

    public async Task UpdateProfileAsync(string corporateEmail, ProfileUpdatingParameters updatingParameters)
    {
        await _profileUpdateCommand.ExecuteAsync(corporateEmail, updatingParameters);
    }

    public async Task DismissAsync(long id)
    {
        await _employeeDismissalTransaction.ExecuteAsync(id);
    }
}
