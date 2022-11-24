using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceService
    {
        private readonly UpdateFinancesCommandHandler _updateFinancesCommandHandler;
        private readonly CoefficientOptions _coefficientOptions;
        private readonly IClock _clock;

        public FinanceService(UpdateFinancesCommandHandler updateFinancesCommandHandler,
            IOptions<CoefficientOptions> coefficientOptions, 
            IClock clock)
        {
            _updateFinancesCommandHandler = updateFinancesCommandHandler;
            _coefficientOptions = coefficientOptions.Value;
            _clock = clock;
        }

        public async Task UpdateFinances(FinanceUpdatingParameters parameters)
        {
            var financeForPayroll = new EmployeeFinanceForPayroll(parameters.RatePerHour, 
                parameters.Pay,
                parameters.EmploymentType, 
                parameters.HasParking);

            var metrics = CalculateMetrics(parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentTypeValue,
                parameters.HasParking);

            await _updateFinancesCommandHandler.Handle(parameters.EmployeeId, financeForPayroll, metrics);
        }

        public EmployeeFinancialMetrics CalculateMetrics(double ratePerHour,
            double pay,
            double employmentTypeValue,
            bool hasParking)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(
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
