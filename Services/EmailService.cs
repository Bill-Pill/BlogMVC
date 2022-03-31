using BlogMVC.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlogMVC.Services
{
    public class EmailService : IBlogEmailSender
    {
        private readonly IConfiguration _config;

        public EmailService( IConfiguration config)
        {
            _config = config;
        }

        public async Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_config["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(_config["MailSettings:Mail"]));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = $"<b>{name}</b> has sent you an email and can be reached at: <b>{emailFrom}</b><br/><br/>{htmlMessage}";

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_config["MailSettings:Host"], Int32.Parse(_config["MailSettings:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["MailSettings:Mail"], _config["MailSettings:Password"]);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_config["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;

            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_config["MailSettings:Host"], Int32.Parse(_config["MailSettings:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["MailSettings:Mail"], _config["MailSettings:Password"]);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
