using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FakeRequestService : IRequestService
    {
        public async Task SendRequestToRegister(Employee employee)
        {
            await Task.CompletedTask;
        }
    }
}