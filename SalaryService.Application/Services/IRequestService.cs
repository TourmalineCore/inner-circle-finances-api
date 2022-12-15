namespace SalaryService.Application.Services
{
    public interface IRequestService
    {
        Task SendPostRequest(string url, object? requestBody);
    }
}
