using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using GymTracker.Core.Entities;
using GymTracker.Application.Interfaces;

namespace GymTracker.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendConfirmationEmailAsync(User user, string token)
        {
            var confirmationLink = GenerateConfirmationLink(token);
            var htmlContent = BuildConfirmationEmailHtml(user.FirstName, confirmationLink);
            await SendEmailAsync(user.Email, "Confirm your email", htmlContent);
        }
        private async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _config["EmailSettings:SenderName"],
                _config["EmailSettings:SenderEmail"]
            ));

            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlContent };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], 587, false);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        private string GenerateConfirmationLink(string token)
        {
            return $"http://localhost:5000/api/auth/confirm-email?token={token}";
        }

        private string BuildConfirmationEmailHtml(string firstName, string confirmationLink)
        {
            return $@"
                <html>
                    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; color: #333; padding: 20px;'>
                        <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); text-align: center;'>
                            <h2 style='color: #6199bc;'>Welcome to GymTracker!</h2>
                            <p>Hi {firstName},</p>
                            <p>To complete your registration, please confirm your email by clicking the link below:</p>
                            <div style='margin: 30px 0;'>
                                <a href='{confirmationLink}' style='background-color: #6199bc; color: white; padding: 12px 24px; border-radius: 5px; text-decoration: none; font-weight: bold;'>Confirm Email</a>
                            </div>
                            <p>If you didn't create an account with GymTracker, please ignore this email.</p>
                            <p style='margin-top: 30px;'>The GymTracker Team 💪</p>
                        </div>
                    </body>
                </html>";
        }
    }
}