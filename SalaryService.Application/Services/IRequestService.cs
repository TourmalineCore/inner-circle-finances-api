using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public interface IRequestService
    {
        Task SendRequestToRegister(Employee employee, string securityCode);
        Task SendPasswordCreatingLink(Employee employee, string securityCode);
    }
}
