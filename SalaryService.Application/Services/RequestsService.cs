using Microsoft.Extensions.Options;
using SalaryService.Domain;
using System.Net.Http.Json;

namespace SalaryService.Application.Services
{
    public class RequestsService : IRequestService
    {
        private readonly HttpClient _client;
        private readonly InnerCircleServiceUrls _urls;

        public RequestsService(IOptions<InnerCircleServiceUrls> urls)
        {
            _client = new HttpClient();
            _urls = urls.Value;
        }

        public async Task SendRequestToRegister(Employee employee)
        {
            var registrationLink = $"{_urls.AuthServiceUrl}api/register";
            await _client.PostAsJsonAsync(registrationLink,
                new { Login = employee.CorporateEmail, PersonalEmail = employee.PersonalEmail });
        }
    }
}
