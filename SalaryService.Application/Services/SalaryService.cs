using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services.HelpServices;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public partial class SalaryServiceParameters
    {
        public long EmployeeId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }
        
        public double EmploymentTypeValue => EmploymentType == EmploymentTypes.FullTime ? 1.0 : 0.5;

        public bool HasParking { get; set; }
    }

    public class EmployeeSalaryService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly FakeTaxService _fakeTaxService;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly CreateBasicSalaryParametersCommandHandler _createBasicSalaryParametersCommandHandler;
        private readonly UpdateBasicSalaryParametersCommandHandler _updateBasicSalaryParametersCommandHandler;
        private readonly UpdateFinancialMetricsCommandHandler _updateFinancialMetricsCommandHandler;
        private readonly CreateHistoryMetricsCommandHandler _createHistoryMetricsCommandHandler;
        private readonly IClock _clock;

        public EmployeeSalaryService(EmployeeRepository employeeRepository,
            FakeTaxService fakeTaxService,
            EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository,
            CreateBasicSalaryParametersCommandHandler createBasicSalaryParametersCommandHandler, 
            CreateEmployeeCommandHandler createEmployeeCommandHandler, 
            UpdateBasicSalaryParametersCommandHandler updateBasicSalaryParametersCommandHandler,
            UpdateFinancialMetricsCommandHandler updateFinancialMetricsCommandHandler,
            CreateHistoryMetricsCommandHandler createHistoryMetricsCommandHandler,
            IClock clock)
        {
            _employeeRepository = employeeRepository;
            _fakeTaxService = fakeTaxService;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _createBasicSalaryParametersCommandHandler = createBasicSalaryParametersCommandHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateBasicSalaryParametersCommandHandler = updateBasicSalaryParametersCommandHandler;
            _updateFinancialMetricsCommandHandler = updateFinancialMetricsCommandHandler;
            _createHistoryMetricsCommandHandler = createHistoryMetricsCommandHandler;
            _clock = clock;
        }

        private EmployeeFinancialMetrics CalculateMetrics(SalaryServiceParameters parameters)
        {
            var calculatedSalaryData = new EmployeeFinancialMetrics(parameters.EmployeeId, 
                parameters.RatePerHour, 
                parameters.Pay, 
                parameters.EmploymentTypeValue, 
                parameters.HasParking);

            var districtCoeff = _fakeTaxService.GetChelyabinskDistrictCoeff();
            var personalIncomeTaxPercent =  _fakeTaxService.GetPersonalIncomeTaxPercent();
            var minimalSizeOfSalary = _fakeTaxService.GetMinimalSizeOfSalary();

            calculatedSalaryData.CalculateMetrics(districtCoeff.Result, minimalSizeOfSalary.Result, personalIncomeTaxPercent.Result,
                new MetricsPeriod(_clock.GetCurrentInstant(), null));

            return calculatedSalaryData;
        }
        private Task<long> CreateEmployeePersonalInfo(SalaryServiceParameters parameters)
        {
            return _createEmployeeCommandHandler.Handle(
                new CreateEmployeeCommand
                {
                    Name = parameters.Name,
                    Surname = parameters.Surname,
                    WorkEmail = parameters.WorkEmail,
                    PersonalEmail = parameters.PersonalEmail,
                    Phone = parameters.Phone,
                    Skype = parameters.Skype,
                    Telegram = parameters.Telegram
                }
            );
        }

        private Task<long> CreateEmployeeBasicInfo(SalaryServiceParameters parameters)
        {
            return _createBasicSalaryParametersCommandHandler.Handle(
                new CreateBasicSalaryParametersCommand
                {
                    EmployeeId = parameters.EmployeeId,
                    RatePerHour = parameters.RatePerHour,
                    Pay = parameters.Pay,
                    EmploymentType = parameters.EmploymentType,
                    HasParking = parameters.HasParking
                }
            );
        }
        public Task CreateEmployee(SalaryServiceParameters parameters)
        {
            CreateEmployeePersonalInfo(parameters);
            CreateEmployeeBasicInfo(parameters);

            var calculatedMetrics = CalculateMetrics(parameters);

            return _employeeFinancialMetricsRepository.CreateAsync(calculatedMetrics);
        }

        private async Task UpdateBasicInfo(SalaryServiceParameters parameters)
        {
            await _updateBasicSalaryParametersCommandHandler.Handle(new UpdateBasicSalaryParametersCommand
            {
                EmployeeId = parameters.EmployeeId,
                RatePerHour = parameters.RatePerHour,
                Pay = parameters.Pay,
                EmploymentType = parameters.EmploymentType,
                HasParking = parameters.HasParking
            });
        }

        private async Task UpdateMetrics(SalaryServiceParameters parameters)
        {
            var newMetrics = CalculateMetrics(parameters);
            await _updateFinancialMetricsCommandHandler.Handle(new UpdateFinancialMetricsCommand
            {
                EmployeeId = newMetrics.EmployeeId,
                RatePerHour = newMetrics.RatePerHour,
                Pay = newMetrics.Pay,
                EmploymentType = newMetrics.EmploymentType,
                HasParking = newMetrics.HasParking,
                Salary = newMetrics.Salary,
                GrossSalary = newMetrics.GrossSalary,
                NetSalary = newMetrics.NetSalary,
                Earnings = newMetrics.Earnings,
                Expenses = newMetrics.Expenses,
                HourlyCostFact = newMetrics.HourlyCostFact,
                HourlyCostHand = newMetrics.HourlyCostHand,
                Retainer = newMetrics.Retainer,
                Profit = newMetrics.Profit,
                ProfitAbility = newMetrics.ProfitAbility
            });
        }

        public Task<long> CreateHistoryRecord(SalaryServiceParameters parameters)
        {
            return _createHistoryMetricsCommandHandler.Handle(new CreateHistoryMetricsCommand
            {
                EmployeeId = parameters.EmployeeId
            });
        }

        public async Task UpdateEmployee(SalaryServiceParameters parameters)
        {
            await CreateHistoryRecord(parameters);
            await UpdateBasicInfo(parameters);
            await UpdateMetrics(parameters);
        }
    }
}
