namespace SalaryService.Domain
{
    public class EmployeeFinancialMetrics : AbstractMetricsEntity
    {
        public EmployeeFinancialMetrics(long employeeId, double ratePerHour, double pay, double employmentType, bool hasParking) : 
            base(employeeId, ratePerHour, pay, employmentType, hasParking)
        {

        }

        public void Update(double salary, 
            double grossSalary, 
            double netSalary, 
            double earnings, 
            double expenses, 
            double hourlyCostFact, 
            double hourlyCostHand, 
            double retainer, 
            double profit, 
            double profitability, 
            double ratePerHour, 
            double pay, 
            double employmentType, 
            bool hasParking)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            HasParking = hasParking;
            Salary = salary;
            GrossSalary = grossSalary;
            NetSalary = netSalary;
            Earnings = earnings;
            Expenses = expenses;
            HourlyCostFact = hourlyCostFact;
            HourlyCostHand = hourlyCostHand;
            Retainer = retainer;
            Profit = profit;
            ProfitAbility = profitability;
        }
    }
}

