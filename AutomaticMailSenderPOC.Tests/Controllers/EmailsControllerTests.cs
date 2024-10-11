using AutomaticMailSenderPOC.Controllers;
using AutomaticMailSenderPOC.DTOs;
using AutomaticMailSenderPOC.Interfaces;
using AutomaticMailSenderPOC.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace AutomaticMailSenderPOC.Tests.Controllers;

public class EmailsControllerTests
{
    private readonly EmailsController _emailsController;
    private readonly ISendEmail _sendMail;

    public EmailsControllerTests()
    {
        _sendMail = A.Fake<ISendEmail>();

        _emailsController = new EmailsController(_sendMail);
    }

    [Fact]
    public async Task When_ValidEmailIsProvided_Should_ReturnOk()
    {
        // Arrange
        var emailRequest = new BasicEmailRequest("John Doe", "johndoe@mail.com", "Test Subject", "Test Body");
        var basicEmailResponse = new BasicEmailResponse
        {
            RecipientName = emailRequest.RecipientName,
            RecipientEmail = emailRequest.Email,
            Subject = emailRequest.Subject,
            Body = emailRequest.Body
        };

        var serviceResponse = new ServiceResponse<BasicEmailResponse>
        {
            Data = basicEmailResponse
        };

        A.CallTo(() => _sendMail.SendEmailAsync(emailRequest))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _emailsController.SendEmailAsync(emailRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        serviceResponse.Success.Should().BeTrue();
        serviceResponse.Message.Should().BeEmpty();
    }
}
