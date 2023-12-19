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

    private const string PaidStatusName = "paid";

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

        var compensationList = compensations.Select(x => new PersonalCompensationItemDto(x.Id, x.Comment, x.Amount, x.IsPaid, x.DateCreateCompensation.ToString(), x.DateCompensation.ToString())).ToList();

        var totalUnpaidAmount = Math.Round(compensations.Sum(x => x.Amount), 2);

        var compensationsResponseList = new PersonalCompensationListDto(compensationList, totalUnpaidAmount);

        return compensationsResponseList;
    }

    public async Task CreateAsync(CompensationCreateDto dto, Employee employee)
    {
        await _compensationCreationCommand.ExecuteAsync(dto, employee);
    }

    public async Task<AllCompensationsListDto> GetAdminAllAsync(int year, int month)
    {
        var compensations = await _compensationsQuery.GetCompensationsAsync(year, month);
        return new AllCompensationsListDto(compensations);
    }

    public async Task UpdateStatusAsync(string status, long[] compensationsIds)
    {
        foreach (var compensationId in compensationsIds)
        {
            var compensation = await _compensationsQuery.FindCompensationByIdAsync(compensationId);

            if (compensation == null)
            {
                throw new Exception($"Compensation with id = {compensationId} not found");
            }

            compensation.IsPaid = status == PaidStatusName;

            await _compensationStatusUpdateCommand.ExecuteAsync(compensation);
        }
    }
}
