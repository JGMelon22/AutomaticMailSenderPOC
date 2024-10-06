using AutomaticMailSenderPOC.DTOs;
using AutomaticMailSenderPOC.Interfaces;
using AutomaticMailSenderPOC.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AutomaticMailSenderPOC.Services;

public class SendEmail : ISendEmail
{
    private readonly SmtpSettings _smtpSettings;

    public SendEmail(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task<ServiceResponse<BasicEmailResponse>> SendEmailAsync(BasicEmailRequest basicEmail)
    {
        var serviceResponse = new ServiceResponse<BasicEmailResponse>();

        try
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(basicEmail.RecipientName, basicEmail.Email));
            message.Subject = basicEmail.Subject;
            message.Body = new TextPart("plain")
            {
                Text = basicEmail.body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("sandbox.smtp.mailtrap.io", 2525, false);

                await client.AuthenticateAsync(_smtpSettings.MailTrapUserName, _smtpSettings.MailTrapPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            serviceResponse.Data = new BasicEmailResponse
            {
                RecipientName = basicEmail.RecipientName, // Using recipient name directly from request
                RecipientEmail = basicEmail.Email,
                Subject = basicEmail.Subject
            };
        }

        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }
}
