using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FakeInnerCircleHttpClient : IInnerCircleHttpClient
    {
        public async Task SendRequestToRegister(Employee employee)
        {
            await Task.CompletedTask;
        }
    }
}