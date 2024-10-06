using MimeKit;
using AutomaticMailSenderPOC.Interfaces;
using AutomaticMailSenderPOC.DTOs;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using AutomaticMailSenderPOC.Configuration;
using AutomaticMailSenderPOC.Models;

namespace AutomaticMailSenderPOC.Services
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SendEmail> _logger;

        public SendEmail(IOptions<SmtpSettings> smtpSettings, ILogger<SendEmail> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task<ServiceResponse<BasicEmailResponse>> SendEmailAsync(BasicEmailRequest basicEmail)
        {
            var serviceResponse = new ServiceResponse<BasicEmailResponse>();
            byte counter = 0;

            try
            {
                _logger.LogInformation("Preparing to send 100 emails....");

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                    await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.Password);

                    for (counter = 0; counter < 100; counter++)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                        message.To.Add(new MailboxAddress(basicEmail.RecipientName, basicEmail.Email));
                        message.Subject = $"{basicEmail.Subject} (Email {counter + 1})";
                        message.Body = new TextPart("plain")
                        {
                            Text = basicEmail.Body
                        };

                        await client.SendAsync(message);

                        _logger.LogInformation($"Email {counter + 1} sent successfully to {basicEmail.Email}");
                    }

                    await client.DisconnectAsync(true);
                }

                serviceResponse.Data = new BasicEmailResponse
                {
                    RecipientName = basicEmail.RecipientName,
                    RecipientEmail = basicEmail.Email,
                    Subject = basicEmail.Subject
                };

                serviceResponse.Message = "Emails sent successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;

                _logger.LogError($"Error while sending emails: {ex.Message}");
            }

            return serviceResponse;
        }
    }
}
