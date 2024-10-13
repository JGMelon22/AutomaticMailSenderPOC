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

        public async Task<ServiceResponse<BasicEmailResponse>> SendEmailAsync(BasicEmailRequest basicEmail, sbyte amount)
        {
            var serviceResponse = new ServiceResponse<BasicEmailResponse>();
            sbyte counter = 1;

            try
            {
                if (amount < 1 || amount > 120)
                    amount = 1;

                _logger.LogInformation($"Preparing to send  {amount} emails....");

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                    await client.AuthenticateAsync(_smtpSettings.MailTrapUserName, _smtpSettings.MailTrapPassword);

                    do
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                        message.To.Add(new MailboxAddress(basicEmail.RecipientName, basicEmail.Email));
                        message.Subject = $"{basicEmail.Subject} (Email {counter})";
                        message.Body = new TextPart("plain")
                        {
                            Text = basicEmail.Body
                        };

                        await client.SendAsync(message);

                        _logger.LogInformation($"Email {counter} sent successfully to {basicEmail.Email}");

                        counter++;
                    } while (counter <= amount);

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
