using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FakeRequestService : IRequestService
    {
        public async Task SendRequestToRegister(Employee employee, string securityCode)
        {
            await Task.CompletedTask;
        }

        public async Task SendPasswordCreatingLink(Employee employee, string securityCode)
        {
            await Task.CompletedTask;
        }
    }
}