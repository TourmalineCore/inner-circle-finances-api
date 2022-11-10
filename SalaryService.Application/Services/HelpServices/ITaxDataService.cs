

namespace SalaryService.Application.Services.HelpServices
{
    interface ITaxDataService
    {
        Task<double> GetChelyabinskDistrictCoeff();

        Task<double> GetPersonalIncomeTaxPercent();

        Task<double> GetMinimalSizeOfSalary();
    }
}
