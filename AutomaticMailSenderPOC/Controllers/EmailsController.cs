using AutomaticMailSenderPOC.DTOs;
using AutomaticMailSenderPOC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomaticMailSenderPOC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailsController : ControllerBase
{
    private readonly ISendEmail _sendEmail;

    public EmailsController(ISendEmail sendEmail)
    {
        _sendEmail = sendEmail;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendEmailAsync([FromQuery] BasicEmailRequest basicEmail)
    {
        var email = await _sendEmail.SendEmailAsync(basicEmail);
        return email.Data != null
            ? Ok(email)
            : BadRequest(email);
    }
}
