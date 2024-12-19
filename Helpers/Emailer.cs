using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;


using asbEvent.Models;
using asbEvent.Data;
using asbEvent.DTOs;

namespace asbEvent.Helpers;
public class Emailer(IConfiguration configuration, ILogger<Emailer> logger)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<Emailer> _logger = logger;

    public async void SendEmail(SendEmailDTO sendEmailDTO)
    {
        try {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("merlin.automated@gmail.com"));
            email.To.Add(MailboxAddress.Parse(sendEmailDTO.Email));
            email.Subject = "Registration Confirmation: " + sendEmailDTO.EventName;

            var builder = new BodyBuilder
            {
                HtmlBody = $"<h1>{sendEmailDTO.EventName}</h1><p>{sendEmailDTO.EventDesc}</p><p>Start Date and Time: {sendEmailDTO.StartDateTime}</p>"
            };

            // Attach QR Code
            builder.Attachments.Add("qrcode.png", sendEmailDTO.QrCode, new ContentType("image", "png"));

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            var smtpServer = _configuration["SmtpServer"];
            var smtpPort = int.Parse(_configuration["SmtpPort"]);
            var smtpUsername = _configuration["SmtpUsername"];
            var smtpPassword = _configuration["SmtpPassword"];

            await smtp.ConnectAsync(smtpServer, 587, SecureSocketOptions.StartTls);

            // Use the app password for authentication
            await smtp.AuthenticateAsync(smtpUsername, smtpPassword);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email sent successfully to: {Email}", sendEmailDTO.Email);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error sending email to: {Email}", sendEmailDTO.Email);
        }

    }

}