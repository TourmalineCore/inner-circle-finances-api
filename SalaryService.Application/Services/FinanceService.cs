using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public partial class FinanceUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public double EmploymentTypeValue => EmploymentType == EmploymentTypes.FullTime ? 1.0 : 0.5;

        public bool HasParking { get; set; }
    }
    public class FinanceService
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly CreateEmployeeFinanceForPayrollCommandHandler _createEmployeeFinanceForPayrollCommandHandler;
        private readonly CreateHistoryMetricsCommandHandler _createHistoryMetricsCommandHandler;
        private readonly UpdateEmployeeFinanceForPayrollCommandHandler _updateEmployeeFinanceForPayrollCommandHandler;
        private readonly UpdateFinancialMetricsCommandHandler _updateFinancialMetricsCommandHandler;
        private readonly DeleteEmployeeFinanceForPayrollCommandHandler _deleteEmployeeFinanceForPayrollCommandHandler;
        private readonly DeleteEmployeeFinancialMetricsCommandHandler _deleteEmployeeFinancialMetricsCommandHandler;
        private readonly CoefficientOptions _coefficientOptions;
        private readonly IClock _clock;

        public FinanceService(EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository, 
            CreateEmployeeFinanceForPayrollCommandHandler createEmployeeFinanceForPayrollCommandHandler, 
            CreateHistoryMetricsCommandHandler createHistoryMetricsCommandHandler, 
            UpdateEmployeeFinanceForPayrollCommandHandler updateEmployeeFinanceForPayrollCommandHandler, 
            UpdateFinancialMetricsCommandHandler updateFinancialMetricsCommandHandler, 
            DeleteEmployeeFinanceForPayrollCommandHandler deleteEmployeeFinanceForPayrollCommandHandler, 
            DeleteEmployeeFinancialMetricsCommandHandler deleteEmployeeFinancialMetricsCommandHandler, 
            IOptions<CoefficientOptions> coefficientOptions, 
            IClock clock)
        {
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _createEmployeeFinanceForPayrollCommandHandler = createEmployeeFinanceForPayrollCommandHandler;
            _createHistoryMetricsCommandHandler = createHistoryMetricsCommandHandler;
            _updateEmployeeFinanceForPayrollCommandHandler = updateEmployeeFinanceForPayrollCommandHandler;
            _updateFinancialMetricsCommandHandler = updateFinancialMetricsCommandHandler;
            _deleteEmployeeFinanceForPayrollCommandHandler = deleteEmployeeFinanceForPayrollCommandHandler;
            _deleteEmployeeFinancialMetricsCommandHandler = deleteEmployeeFinancialMetricsCommandHandler;
            _coefficientOptions = coefficientOptions.Value;
            _clock = clock;
        }

        public Task<long> CreateEmployeeFinanceForPayroll(long employeeId, double ratePerHour, double pay, EmploymentTypes employmentType, bool hasParking)
        {
            return _createEmployeeFinanceForPayrollCommandHandler.Handle(
                new CreateEmployeeFinanceForPayrollCommand
                {
                    EmployeeId = employeeId,
                    RatePerHour = ratePerHour,
                    Pay = pay,
                    EmploymentType = employmentType,
                    HasParking = hasParking
                }
            );
        }

        public Task<long> CreateMetrics(long employeeId,
            double ratePerHour,
            double pay,
            double employmentTypeValue,
            bool hasParking)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking);

            calculateMetrics.CalculateMetrics(_coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _clock.GetCurrentInstant());
            return _employeeFinancialMetricsRepository.CreateAsync(calculateMetrics);
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            await CreateHistoryRecord(parameters.EmployeeId);

            await UpdateEmployeeFinanceForPayroll(parameters.EmployeeId,
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.HasParking);

            await UpdateMetrics(parameters.EmployeeId,
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.HasParking);
        }

        public async Task DeleteFinances(long id)
        {
            await _createHistoryMetricsCommandHandler.Handle(id);
            await _deleteEmployeeFinancialMetricsCommandHandler.Handle(id);
            await _deleteEmployeeFinanceForPayrollCommandHandler.Handle(id);
        }

        private async Task UpdateEmployeeFinanceForPayroll(long employeeId, double ratePerHour, double pay, EmploymentTypes employmentType, bool hasParking)
        {
            await _updateEmployeeFinanceForPayrollCommandHandler.Handle(new UpdateEmployeeFinanceForPayrollCommand
            {
                EmployeeId = employeeId,
                RatePerHour = ratePerHour,
                Pay = pay,
                EmploymentType = employmentType,
                HasParking = hasParking
            });
        }

        private async Task UpdateMetrics(long employeeId,
            double ratePerHour,
            double pay,
            double employmentTypeValue,
            bool hasParking)
        {
            var newMetrics = CalculateMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking);

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
                IncomeTaxContributions = newMetrics.IncomeTaxContributions,
                PensionContributions = newMetrics.PensionContributions,
                MedicalContributions = newMetrics.MedicalContributions,
                SocialInsuranceContributions = newMetrics.SocialInsuranceContributions,
                InjuriesContributions = newMetrics.InjuriesContributions,
                Expenses = newMetrics.Expenses,
                HourlyCostFact = newMetrics.HourlyCostFact,
                HourlyCostHand = newMetrics.HourlyCostHand,
                Retainer = newMetrics.Retainer,
                Profit = newMetrics.Profit,
                ProfitAbility = newMetrics.ProfitAbility
            });
        }

        private Task<long> CreateHistoryRecord(long employeeId)
        {
            return _createHistoryMetricsCommandHandler.Handle(employeeId);
        }

        private EmployeeFinancialMetrics CalculateMetrics(long employeeId,
            double ratePerHour,
            double pay,
            double employmentTypeValue,
            bool hasParking)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking);

            calculateMetrics.CalculateMetrics(_coefficientOptions.DistrictCoefficient,
                _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent,
                _clock.GetCurrentInstant());

            return calculateMetrics;
        }
    }
}
