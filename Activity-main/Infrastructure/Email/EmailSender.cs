using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendPasswordResetEmailAsync(string email, string token)
        {
            var smtpClient = new SmtpClient(_emailConfig.Host)
            {
                Port = int.Parse(_emailConfig["Smtp:Port"]),
                Credentials = new NetworkCredential(_emailConfig["Smtp:UserName"], _emailConfig["Smtp:Password"]),
                EnableSsl = bool.Parse(_emailConfig["Smtp:EnableSsl"])
            };

            var from = new MailAddress(_emailConfig["Smtp:SenderEmail"], _emailConfig["Smtp:SenderName"]);
            var to = new MailAddress(email);
            var mailMessage = new MailMessage
            {
                From = from,
                Subject = "Password Reset Request",
                Body = $"<strong>Apiease reset your password using the following token:</strong> <br/><br/> <a href=\"https://yourapp.com/reset-password?token={token}\">Reset Password</a>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
