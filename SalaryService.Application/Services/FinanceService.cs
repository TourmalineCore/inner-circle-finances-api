using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Dtos;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly CalculateTotalExpensesCommandHandler _calculateTotalExpensesCommandHandler;
        private readonly CoefficientOptions _coefficientOptions;
        private readonly IClock _clock;

        public FinanceAnalyticService(CalculateTotalExpensesCommandHandler calculateTotalExpensesCommandHandler,
            IOptions<CoefficientOptions> coefficientOptions, 
            IClock clock)
        {
            _calculateTotalExpensesCommandHandler = calculateTotalExpensesCommandHandler;
            _coefficientOptions = coefficientOptions.Value;
            _clock = clock;
        }

        public Task CalculateTotalFinances()
        {
            return _calculateTotalExpensesCommandHandler.Handle();
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
