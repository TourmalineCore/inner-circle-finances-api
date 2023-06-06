namespace SalaryService.Domain;

public static class MetricsDiffCalculator
{
    public static EmployeeFinancialMetricsDiff CalculateDiffBetweenEmployeeFinancialMetrics(EmployeeFinancialMetrics sourceMetrics, EmployeeFinancialMetrics newMetrics)
    {
        return new EmployeeFinancialMetricsDiff
        {
            RatePerHour = newMetrics.RatePerHour - sourceMetrics.RatePerHour,
            Pay = newMetrics.Pay - sourceMetrics.Pay,
            ParkingCostPerMonth = newMetrics.ParkingCostPerMonth - sourceMetrics.ParkingCostPerMonth,
            Salary = newMetrics.Salary - sourceMetrics.Salary,
            AccountingPerMonth = newMetrics.AccountingPerMonth - sourceMetrics.AccountingPerMonth,
            HourlyCostFact = newMetrics.HourlyCostFact - sourceMetrics.HourlyCostFact,
            HourlyCostHand = newMetrics.HourlyCostHand - sourceMetrics.HourlyCostHand,
            Earnings = newMetrics.Earnings - sourceMetrics.Earnings,
            Expenses = newMetrics.Expenses - sourceMetrics.Expenses,
            IncomeTaxContributions = newMetrics.IncomeTaxContributions - sourceMetrics.IncomeTaxContributions,
            DistrictCoefficient = newMetrics.DistrictCoefficient - sourceMetrics.DistrictCoefficient,
            PensionContributions = newMetrics.PensionContributions - sourceMetrics.PensionContributions,
            MedicalContributions = newMetrics.MedicalContributions - sourceMetrics.MedicalContributions,
            SocialInsuranceContributions =
                newMetrics.SocialInsuranceContributions - sourceMetrics.SocialInsuranceContributions,
            InjuriesContributions = newMetrics.InjuriesContributions - sourceMetrics.InjuriesContributions,
            Profit = newMetrics.Profit - sourceMetrics.Profit,
            ProfitAbility = newMetrics.ProfitAbility - sourceMetrics.ProfitAbility,
            GrossSalary = newMetrics.GrossSalary - sourceMetrics.GrossSalary,
            Prepayment = newMetrics.Prepayment - sourceMetrics.Prepayment,
            NetSalary = newMetrics.NetSalary - sourceMetrics.NetSalary,
        };
    }

    public static TotalEmployeeFinancialMetricsEntry CalculateTotalEmployeeFinancialMetrics(
        IEnumerable<EmployeeFinancialMetrics> metrics)
    {
        return new TotalEmployeeFinancialMetricsEntry
        {
            ParkingCostPerMonth = metrics.Sum(x => x.ParkingCostPerMonth),
            AccountingPerMonth = metrics.Sum(x => x.AccountingPerMonth),
            Earnings = metrics.Sum(x => x.Earnings),
            Expenses = metrics.Sum(x => x.Expenses),
            IncomeTaxContributions = metrics.Sum(x => x.IncomeTaxContributions),
            PensionContributions = metrics.Sum(x => x.PensionContributions),
            MedicalContributions = metrics.Sum(x => x.MedicalContributions),
            SocialInsuranceContributions = metrics.Sum(x => x.SocialInsuranceContributions),
            InjuriesContributions = metrics.Sum(x => x.InjuriesContributions),
            Profit = metrics.Sum(x => x.Profit),
            Prepayment = metrics.Sum(x => x.Prepayment),
            NetSalary = metrics.Sum(x => x.NetSalary),
        };
    }

    public static EmployeeFinancialTotalMetricsDiff CalculateDiffBetweenTotalEmployeeFinancialMetrics(
        TotalEmployeeFinancialMetricsEntry sourceTotalMetrics, TotalEmployeeFinancialMetricsEntry newTotalMetrics)
    {
        return new EmployeeFinancialTotalMetricsDiff
        {
            ParkingCostPerMonth = newTotalMetrics.ParkingCostPerMonth - sourceTotalMetrics.ParkingCostPerMonth,
            AccountingPerMonth = newTotalMetrics.AccountingPerMonth - sourceTotalMetrics.AccountingPerMonth,
            Earnings = newTotalMetrics.Earnings - sourceTotalMetrics.Earnings,
            Expenses = newTotalMetrics.Expenses - sourceTotalMetrics.Expenses,
            IncomeTaxContributions = newTotalMetrics.IncomeTaxContributions - sourceTotalMetrics.IncomeTaxContributions,
            PensionContributions = newTotalMetrics.PensionContributions - sourceTotalMetrics.PensionContributions,
            MedicalContributions = newTotalMetrics.MedicalContributions - sourceTotalMetrics.MedicalContributions,
            SocialInsuranceContributions = newTotalMetrics.SocialInsuranceContributions - sourceTotalMetrics.SocialInsuranceContributions,
            InjuriesContributions = newTotalMetrics.InjuriesContributions - sourceTotalMetrics.InjuriesContributions,
            Profit = newTotalMetrics.Profit - sourceTotalMetrics.Profit,
            ProfitAbility = newTotalMetrics.ProfitAbility - sourceTotalMetrics.ProfitAbility,
            Prepayment = newTotalMetrics.Prepayment - sourceTotalMetrics.Prepayment,
            NetSalary = newTotalMetrics.NetSalary - sourceTotalMetrics.NetSalary,
        };
    }
}
