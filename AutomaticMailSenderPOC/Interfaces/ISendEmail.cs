using AutomaticMailSenderPOC.DTOs;
using AutomaticMailSenderPOC.Models;

namespace AutomaticMailSenderPOC.Interfaces;

public interface ISendEmail
{
    Task<ServiceResponse<BasicEmailResponse>> SendEmailAsync(BasicEmailRequest basicEmail);
}
