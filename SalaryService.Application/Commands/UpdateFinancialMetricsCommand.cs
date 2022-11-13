using Newtonsoft.Json;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class UpdateFinancialMetricsCommand
    {
        public long EmployeeId { get; set; }

        private double salary;
        
        public double Salary { get { return salary; } set { if (value >= 0) salary = value; else throw new ArgumentException(); } }

        private double hourlyCostFact;
        
        public double HourlyCostFact { get { return hourlyCostFact; } set { if (value >= 0) hourlyCostFact = value; else throw new ArgumentException(); } }

        private double hourlyCostHand;
        
        public double HourlyCostHand { get { return hourlyCostHand; } set { if (value >= 0) hourlyCostHand = value; else throw new ArgumentException(); } }

        private double earnings;
        
        public double Earnings { get { return earnings; } set { if (value >= 0) earnings = value; else throw new ArgumentException(); } }

        private double expenses;
        
        public double Expenses { get { return expenses; } set { if (value >= 0) expenses = value; else throw new ArgumentException(); } }

        private double profit;
        
        public double Profit { get { return profit; } set { if (value >= 0) profit = value; else throw new ArgumentException(); } }

        private double profitability;
        
        public double ProfitAbility { get { return profitability; } set { if (value >= 0) profitability = value; else throw new ArgumentException(); } }

        private double grossSalary;
        
        public double GrossSalary { get { return grossSalary; } set { if (value >= 0) grossSalary = value; else throw new ArgumentException(); } }

        private double netSalary;
        
        public double NetSalary { get { return netSalary; } set { if (value >= 0) netSalary = value; else throw new ArgumentException(); } }

        private double ratePerHour;

        public double RatePerHour { get { return ratePerHour; } set { if (value >= 0) ratePerHour = value; else throw new ArgumentException(); } }

        private double pay;

        public double Pay { get { return pay; } set { if (value >= 0) pay = value; else throw new ArgumentException(); } }

        private double retainer;
        
        public double Retainer { get { return retainer; } set { if (value >= 0) retainer = value; else throw new ArgumentException(); } }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }
    public class UpdateFinancialMetricsCommandHandler
    {
        private readonly EmployeeFinancialMetricsRepository _employeeFinancialMetricsRepository;

        public UpdateFinancialMetricsCommandHandler(EmployeeFinancialMetricsRepository basicSalaryParametersRepository)
        {
            _employeeFinancialMetricsRepository = basicSalaryParametersRepository;
        }

        public async Task Handle(UpdateFinancialMetricsCommand request)
        {
            var salaryMetrics = _employeeFinancialMetricsRepository.GetById(request.EmployeeId).Result;
            salaryMetrics.Update(request.Salary,
                request.HourlyCostFact,
                request.HourlyCostHand,
                request.Earnings,
                request.Expenses,
                request.Profit,
                request.ProfitAbility,
                request.GrossSalary,
                request.NetSalary,
                request.RatePerHour,
                request.Pay,
                request.Retainer,
                request.EmploymentType,
                request.HasParking);

            await _employeeFinancialMetricsRepository.UpdateAsync(salaryMetrics);
        }
    }
}
