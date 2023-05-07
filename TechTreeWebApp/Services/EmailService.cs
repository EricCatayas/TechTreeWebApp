using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<bool> SendEmail(string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(recipientEmail));
            message.To.Add(MailboxAddress.Parse(_config["UserEmail"]));

            message.Subject = $"TechTree Mail! from {recipientEmail}";

            message.Body = new TextPart("plain")
            {
                Text = body
            };
            try
            {
                using (var smtp = new SmtpClient())
                {
                    // Setup 2-Step Verification to be turned on in myaccount.google
                    // Generate App Password in myaccount.google : use that password

                    // NotSupportedException is thrown if failed
                    smtp.Connect(_config["EmailHost"], 587, SecureSocketOptions.StartTls);

                    smtp.Authenticate(_config["UserEmail"], _config["EMAILSERVICE_APPPASSWORD"]);

                    smtp.SendAsync(message).Wait();
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
