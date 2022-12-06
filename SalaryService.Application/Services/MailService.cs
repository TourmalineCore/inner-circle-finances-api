using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace SalaryService.Application.Services
{
    public class MailService
    {
        private readonly MailOptions _mailOptions;

        public MailService(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }
        
        public static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijqklnoprstuvwxyz0123456789!#@%&*-_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public void SendCredentials(string personalEmail, string corporateEmail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.mail.ru";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_mailOptions.MailAddress, _mailOptions.MailPassword);
            client.Send(_mailOptions.MailAddress, personalEmail, null, $"Your credentials: \nEmail: {corporateEmail} \nPassword: {GeneratePassword(15)}");
        }
    }
}
