using Microsoft.Extensions.Options;
using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public partial class EmployeeCreatingParameters
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

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
    public partial class EmployeeUpdatingParameters
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }
        
        public double EmploymentTypeValue => EmploymentType == EmploymentTypes.FullTime ? 1.0 : 0.5;

        public bool HasParking { get; set; }
    }

    public class EmployeeFinanceService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly CreateEmployeeFinanceForPayrollCommandHandler _createEmployeeFinanceForPayrollCommandHandler;
        private readonly UpdateEmployeeFinanceForPayrollCommandHandler _updateEmployeeFinanceForPayrollCommandHandler;
        private readonly UpdateFinancialMetricsCommandHandler _updateFinancialMetricsCommandHandler;
        private readonly CreateHistoryMetricsCommandHandler _createHistoryMetricsCommandHandler;
        private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private readonly DeleteEmployeeFinanceForPayrollCommandHandler _deleteEmployeeFinanceForPayrollCommandHandler;
        private readonly DeleteEmployeeFinancialMetricsCommandHandler _deleteEmployeeFinancialMetricsCommandHandler;
        private readonly CoefficientOptions _coefficientOptions;
        private readonly IClock _clock;

        public EmployeeFinanceService(EmployeeRepository employeeRepository,
            EmployeeFinancialMetricsRepository employeeFinancialMetricsRepository,
            EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository,
            CreateEmployeeFinanceForPayrollCommandHandler createEmployeeFinanceForPayrollCommandHandler, 
            CreateEmployeeCommandHandler createEmployeeCommandHandler, 
            UpdateEmployeeFinanceForPayrollCommandHandler updateEmployeeFinanceForPayrollCommandHandler,
            UpdateFinancialMetricsCommandHandler updateFinancialMetricsCommandHandler,
            CreateHistoryMetricsCommandHandler createHistoryMetricsCommandHandler,
            DeleteEmployeeCommandHandler deleteEmployeeCommandHandler,
            DeleteEmployeeFinanceForPayrollCommandHandler deleteEmployeeFinanceForPayrollCommandHandler,
            DeleteEmployeeFinancialMetricsCommandHandler deleteEmployeeFinancialMetricsCommandHandler,
            IOptions<CoefficientOptions> coefficientOptions,
            IClock clock)
        {
            _employeeRepository = employeeRepository;
            _employeeFinancialMetricsRepository = employeeFinancialMetricsRepository;
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
            _createEmployeeFinanceForPayrollCommandHandler = createEmployeeFinanceForPayrollCommandHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeFinanceForPayrollCommandHandler = updateEmployeeFinanceForPayrollCommandHandler;
            _updateFinancialMetricsCommandHandler = updateFinancialMetricsCommandHandler;
            _createHistoryMetricsCommandHandler = createHistoryMetricsCommandHandler;
            _deleteEmployeeCommandHandler = deleteEmployeeCommandHandler;
            _deleteEmployeeFinanceForPayrollCommandHandler = deleteEmployeeFinanceForPayrollCommandHandler;
            _deleteEmployeeFinancialMetricsCommandHandler = deleteEmployeeFinancialMetricsCommandHandler;
            _coefficientOptions = coefficientOptions.Value;
            _clock = clock;
        }

        public async Task DeleteEmployee(long id)
        {
            await CreateHistoryRecord(id);
            await _deleteEmployeeFinancialMetricsCommandHandler.Handle(id);
            await _deleteEmployeeFinanceForPayrollCommandHandler.Handle(id);
            await _deleteEmployeeCommandHandler.Handle(id);
        }

        public async Task CreateEmployee(EmployeeCreatingParameters parameters)
        {
            var employee = await CreateEmployeeProfile(parameters.Name, 
                parameters.Surname, 
                parameters.MiddleName, 
                parameters.WorkEmail, 
                parameters.PersonalEmail, 
                parameters.Phone, 
                parameters.Skype, 
                parameters.Telegram);

           var financeForPayrollId = await CreateEmployeeFinanceForPayroll(employee.Id,
                parameters.RatePerHour,
                parameters.Pay,
                parameters.EmploymentType,
                parameters.HasParking);

           
           var metricsId = await CreateMetrics(employee.Id, 
                parameters.RatePerHour, 
                parameters.Pay, 
                parameters.EmploymentTypeValue, 
                parameters.HasParking, 
                _coefficientOptions.DistrictCoefficient, 
                _coefficientOptions.MinimumWage, 
                _coefficientOptions.IncomeTaxPercent);
            
           await _employeeRepository.AddFinanceForPayrollAndMetrics(employee, financeForPayrollId, metricsId);
        }

        public async Task UpdateEmployee(EmployeeUpdatingParameters parameters)
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
                parameters.HasParking, _coefficientOptions.DistrictCoefficient, _coefficientOptions.MinimumWage,
                _coefficientOptions.IncomeTaxPercent);
        }

        private Task<long> CreateHistoryRecord(long employeeId)
        {
            return _createHistoryMetricsCommandHandler.Handle(new CreateHistoryMetricsCommand
            {
                EmployeeId = employeeId
            });
        }

        private EmployeeFinancialMetrics CalculateMetrics(long employeeId, 
            double ratePerHour, 
            double pay, 
            double employmentTypeValue, 
            bool hasParking, 
            double districtCoefficient, 
            double minimumWage, 
            double incomeTax)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking);

            calculateMetrics.CalculateMetrics(districtCoefficient, minimumWage, incomeTax,
                _clock.GetCurrentInstant());

            return calculateMetrics;
        }

        private Task<long> CreateMetrics(long employeeId,
            double ratePerHour,
            double pay,
            double employmentTypeValue,
            bool hasParking,
            double districtCoefficient,
            double minimumWage,
            double incomeTax)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking);

            calculateMetrics.CalculateMetrics(districtCoefficient, minimumWage, incomeTax,
                _clock.GetCurrentInstant());
            return _employeeFinancialMetricsRepository.CreateAsync(calculateMetrics);
        }

        private Task<Employee> CreateEmployeeProfile(string name, 
            string surname, 
            string middleName, 
            string workEmail, 
            string personalEmail, 
            string phone, 
            string skype, 
            string telegram)
        {
            return _createEmployeeCommandHandler.Handle(
                new CreateEmployeeCommand
                {
                    Name = name,
                    Surname = surname,
                    MiddleName = middleName,
                    WorkEmail = workEmail,
                    PersonalEmail = personalEmail,
                    Phone = phone,
                    Skype = skype,
                    Telegram = telegram
                }
            );
        }

        private Task<long> CreateEmployeeFinanceForPayroll(long employeeId, double ratePerHour, double pay, EmploymentTypes employmentType, bool hasParking)
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
            bool hasParking, 
            double districtCoefficient, 
            double minimumWage, 
            double incomeTax)
        {
            var newMetrics = CalculateMetrics(employeeId,
                ratePerHour,
                pay,
                employmentTypeValue,
                hasParking,
                districtCoefficient,
                minimumWage,
                incomeTax);

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

        
    }
}
