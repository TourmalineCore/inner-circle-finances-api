using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public interface IInnerCircleHttpClient
    {
        Task SendRequestToRegister(Employee employee);
    }
}
