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

        public char GenerateChar(Random rng)
        {
            return (char)(rng.Next('A', 'z' + 1));
        }

        public string GeneratePassword(Random rng, int length)
        {
            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = GenerateChar(rng);
            }
            return new string(letters);
        }

        public void SendCredentials(string personalEmail, string corporateEmail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailOptions.MailAddress);
            mail.To.Add(new MailAddress(personalEmail));
            mail.Subject = "Your credentials";
            mail.Body = $"Email: {corporateEmail} Password: {GeneratePassword(new Random(), 15)}";

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.mail.ru";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_mailOptions.MailAddress, _mailOptions.MailPassword);
            client.Send(mail);
        }
    }
}
