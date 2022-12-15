using System.Net.Http.Json;

namespace SalaryService.Application.Services
{
    public class RequestsService : IRequestService
    {
        private readonly HttpClient _client;

        public RequestsService()
        {
            _client = new HttpClient();
        }

        public async Task SendPostRequest(string url, object? requestBody)
        {
            await _client.PostAsJsonAsync(url, requestBody);
        }
    }
}
