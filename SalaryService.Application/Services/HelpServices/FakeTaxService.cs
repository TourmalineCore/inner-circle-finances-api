
namespace SalaryService.Application.Services.HelpServices
{
    public class FakeTaxService : ITaxDataService
    {
        public Task<double> GetChelyabinskDistrictCoeff()
        {
            return Task.FromResult(0.15);
        }

        public Task<double> GetMinimalSizeOfSalary()
        {
            return Task.FromResult(15279.0);
        }

        public Task<double> GetPersonalIncomeTaxPercent()
        {
            return Task.FromResult(0.13);
        }
    }
}
