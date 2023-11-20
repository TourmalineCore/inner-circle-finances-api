using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class CompensationsService
{
    private readonly CompensationCreationCommand _compensationCreationCommand;

    private readonly ICompensationsQuery _compensationsQuery;

    public CompensationsService(CompensationCreationCommand createCompensationCommandHandler, ICompensationsQuery compensationsQuery)
    {
        _compensationCreationCommand = createCompensationCommandHandler;
        _compensationsQuery = compensationsQuery;
    }

    public async Task<List<CompensationType>> GetTypesAsync()
    {
        return CompensationTypes.GetTypeList();
    }

    public async Task<CompensationListDto> GetAllAsync(string corporateEmail)
    {
        var compensations = await _compensationsQuery.GetPersonalCompensationsAsync(corporateEmail);

        var compensationList = compensations.Select(x => new CompensationItemDto()
        {
            Id = x.Id,
            Comment = x.Comment,
            Amount = x.Amount,
            IsPaid = x.IsPaid,
            DateCreateCompensation = x.DateCreateCompensation.ToString(),
            DateCompensation = x.DateCompensation.ToString()
        }).ToList();

        var totalUnpaidAmount = Math.Round(compensations.Sum(x => x.Amount), 2);

        var compensationsResponseList = new CompensationListDto()
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

    public async Task<CompensationCeoListDto> GetAdminAllAsync()
    {
        var compensations = await _compensationsQuery.GetCompensationsAsync();

        var compensationCeoList = compensations.Select(x => new CompensationCeoItemDto()
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

        var compensationsResponseList = new CompensationCeoListDto()
        {
            List = compensationCeoList,
            TotalAmount = totalAmount
        };

        return compensationsResponseList;
    }

    //public async Task UpdateStatusAsync()
    //{

    //    // await _compensationCreationCommand.ExecuteAsync(dto, employee);
    //}
}
