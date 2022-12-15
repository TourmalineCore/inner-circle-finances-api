namespace SalaryService.Application.Services
{
    public class FakeRequestService : IRequestService
    {
        public async Task SendPostRequest(string url, object? requestBody)
        {
            await Task.CompletedTask;
        }
    }
}