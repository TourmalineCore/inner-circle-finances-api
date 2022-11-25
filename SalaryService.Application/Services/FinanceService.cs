using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        
        private readonly CoefficientOptions _coefficientOptions;
        private readonly IClock _clock;

        public FinanceAnalyticService(IOptions<CoefficientOptions> coefficientOptions, 
            IClock clock)
        {
            _coefficientOptions = coefficientOptions.Value;
            _clock = clock;
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
