
namespace SalaryService.Domain.Common
{
    internal class ThirdPartyServicesPriceConsts
    {
        public const int AccountingPerYear = 7200;

        public const int AccountingPerMonth = AccountingPerYear / 12;

        public const int ParkingCostPerMonth = 1800;

        public const int ParkingCostPerYear = ParkingCostPerMonth * 12;
    }
}
