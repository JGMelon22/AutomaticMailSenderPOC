using MimeKit;
using AutomaticMailSenderPOC.Interfaces;
using AutomaticMailSenderPOC.DTOs;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using AutomaticMailSenderPOC.Configuration;
using AutomaticMailSenderPOC.Models;

namespace AutomaticMailSenderPOC.Services;

public class SendEmail : ISendEmail
{
    // private readonly SmtpSettings _smtpSettings;
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
            var message = new MimeMessage();

            _logger.LogInformation("Sending E-mail....");

            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(basicEmail.RecipientName, basicEmail.Email));
            message.Subject = basicEmail.Subject;
            message.Body = new TextPart("plain")
            {
                Text = basicEmail.Body
            };

            do
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);

                    await client.AuthenticateAsync(_smtpSettings.MailTrapUserName, _smtpSettings.MailTrapPassword);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    counter++;
                }
            }

            while (counter < 10);

            serviceResponse.Data = new BasicEmailResponse
            {
                RecipientName = basicEmail.RecipientName,
                RecipientEmail = basicEmail.Email,
                Subject = basicEmail.Subject
            };
        }

        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;

            _logger.LogError("Something went wrong!");
        }

        return serviceResponse;
    }
}
