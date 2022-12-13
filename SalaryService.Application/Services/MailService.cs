using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace SalaryService.Application.Services
{
    public class MailService
    {
        private readonly MailOptions _mailOptions;
        private readonly SmtpClient _client;

        public MailService(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
            _client = new SmtpClient();
            _client.Host = "smtp.mail.ru";
            _client.Port = 587;
            _client.EnableSsl = true;
            _client.Credentials = new NetworkCredential(_mailOptions.SenderMailAddress, _mailOptions.SenderMailPassword);
        }
        
        public static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijqklnoprstuvwxyz0123456789!#@%&*-_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public void SendCredentials(string personalEmail, string corporateEmail)
        {
            _client.Send(_mailOptions.SenderMailAddress, personalEmail, null, $"Your credentials: \nEmail: {corporateEmail} \nPassword: {GeneratePassword(15)}");
        }

        public void SendWelcomeLink(string personalEmail, string welcomeLink)
        {
            _client.Send(_mailOptions.SenderMailAddress, personalEmail, null, $"Go to this link to set a password for your account: {welcomeLink}");
        }
    }
}
