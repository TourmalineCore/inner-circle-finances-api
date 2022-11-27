namespace SalaryService.Application.Services;

public static class TotalFinances
{
    public static double PayrollExpense { get; set; }
    public static double OfficeExpense { get; set; }
    public static double TotalExpense { get; set; }

    public static double DesiredIncome { get; set; }
    public static double DesiredProfit { get; set; }
    public static double DesiredProfitability { get; set; }

    public static double ReserveForQuarter { get; set; }
    public static double ReserveForHalfYear { get; set; }
    public static double ReserveForYear { get; set; }  

}