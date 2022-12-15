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

        public async Task SendRequestToRegister(Employee employee, string securityCode)
        {
            var registrationLink = $"{_urls.AuthServiceUrl}auth/register";
            await _client.PostAsJsonAsync(registrationLink,
                new { Login = employee.CorporateEmail, Password = "", Code = securityCode });
        }

        public async Task SendPasswordCreatingLink(Employee employee, string securityCode)
        {
            var mailSenderLink = $"{_urls.EmailSenderServiceUrl}api/mail/send";
            await _client.PostAsJsonAsync(mailSenderLink,
            new { To = employee.PersonalEmail, Body = $"Go to this link to set a password for your account: {_urls.AuthUIServiceUrl}invitation?code={securityCode}" });
        }
    }
}
