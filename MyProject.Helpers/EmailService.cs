using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace MyProject.Helpers
{
    public class EmailService
    {
        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpHost"])
            && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SmtpUser"]);

        public void Send(string to, string subject, string body)
        {
            string host = ConfigurationManager.AppSettings["SmtpHost"] ?? "";
            int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
            string user = ConfigurationManager.AppSettings["SmtpUser"] ?? "";
            string password = ConfigurationManager.AppSettings["SmtpPassword"] ?? "";
            string from = ConfigurationManager.AppSettings["SmtpFrom"] ?? user;

            using var message = new MailMessage(from, to, subject, body);
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(user, password)
            };

            client.Send(message);
        }
    }
}