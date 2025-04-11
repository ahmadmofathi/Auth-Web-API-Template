using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TemplateWeb.BL.Managers.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailConfig = _configuration.GetSection("EmailSettings");
            var smtpClient = new SmtpClient(emailConfig["SmtpServer"])
            {
                Port = int.Parse(emailConfig["Port"]!),
                Credentials = new NetworkCredential(emailConfig["Username"], emailConfig["Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailConfig["SenderEmail"]!, emailConfig["SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
