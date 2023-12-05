using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class CompensationsService
{
    private readonly CompensationCreationCommand _compensationCreationCommand;

    private readonly CompensationStatusUpdateCommand _compensationStatusUpdateCommand;

    private readonly ICompensationsQuery _compensationsQuery;

    public CompensationsService(CompensationCreationCommand createCompensationCommandHandler, ICompensationsQuery compensationsQuery, CompensationStatusUpdateCommand compensationStatusUpdateCommand)
    {
        _compensationCreationCommand = createCompensationCommandHandler;
        _compensationsQuery = compensationsQuery;
        _compensationStatusUpdateCommand = compensationStatusUpdateCommand;
    }

    public async Task<List<CompensationType>> GetTypesAsync()
    {
        return CompensationTypes.GetTypeList();
    }

    public async Task<PersonalCompensationListDto> GetAllAsync(string corporateEmail)
    {
        var compensations = await _compensationsQuery.GetPersonalCompensationsAsync(corporateEmail);

        var compensationList = compensations.Select(x => new PersonalCompensationItemDto()
        {
            Id = x.Id,
            Comment = x.Comment,
            Amount = x.Amount,
            IsPaid = x.IsPaid,
            DateCreateCompensation = x.DateCreateCompensation.ToString(),
            DateCompensation = x.DateCompensation.ToString()
        }).ToList();

        var totalUnpaidAmount = Math.Round(compensations.Sum(x => x.Amount), 2);

        var compensationsResponseList = new PersonalCompensationListDto()
        {
            List = compensationList,
            TotalUnpaidAmount = totalUnpaidAmount
        };

        return compensationsResponseList;
    }

    public async Task CreateAsync(CompensationCreateDto dto, Employee employee)
    {
        await _compensationCreationCommand.ExecuteAsync(dto, employee);
    }

    public async Task<CompensationListDto> GetAdminAllAsync()
    {
        var compensations = await _compensationsQuery.GetCompensationsAsync();

        var compensationList = compensations.Select(x => new CompensationItemDto()
        {
            Id = x.Id,
            EmployeeFullName = x.Employee.GetFullName(),
            Comment = x.Comment,
            Amount = x.Amount,
            IsPaid = x.IsPaid,
            DateCreateCompensation = x.DateCreateCompensation.ToString(),
            DateCompensation = x.DateCompensation.ToString()
        }).ToList();

        var totalAmount = Math.Round(compensations.Sum(x => x.Amount), 2);

        var compensationsResponseList = new CompensationListDto()
        {
            List = compensationList,
            TotalAmount = totalAmount
        };

        return compensationsResponseList;
    }

    public async Task UpdateStatusAsync(bool isPaid, long[] compensationsIds)
    {
        foreach(var compensationId in compensationsIds)
        {
            var compensation = await _compensationsQuery.FindCompensationByIdAsync(compensationId);
            compensation.IsPaid = isPaid;

            await _compensationStatusUpdateCommand.ExecuteAsync(compensation);
        }
    }
}
