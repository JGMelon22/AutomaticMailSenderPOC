using AutomaticMailSenderPOC.DTOs;
using AutomaticMailSenderPOC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomaticMailSenderPOC.Controllers;

/// <summary>
/// Controller for handling email operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmailsController : ControllerBase
{
    private readonly ISendEmail _sendEmail;

    public EmailsController(ISendEmail sendEmail)
    {
        _sendEmail = sendEmail;
    }

    /// <summary>
    /// Sends an email based on the provided request.
    /// </summary>
    /// <param name="basicEmail">The details of the email to be sent.</param>
    /// <param name="amount">Amount of emails desired to be sent.</param>
    /// <returns>A task representing the asynchronous operation, with an <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// POST /api/emails?recipientName=Test&amp;email=test@example.com&amp;subject=Test&amp;body=Hello%20World&amp;amount=2
    /// </remarks>
    /// <response code="200">Returns the details of the newly sent email.</response>
    /// <response code="400">If <paramref name="basicEmail"/> is null or invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendEmailAsync([FromQuery] BasicEmailRequest basicEmail, sbyte amount)
    {
        var email = await _sendEmail.SendEmailAsync(basicEmail, amount);
        return email.Data != null
            ? Ok(email)
            : BadRequest(email);
    }
}
