namespace AutomaticMailSenderPOC.DTOs;

public record BasicEmailResponse
{
    public string RecipientName { get; init; } = string.Empty!;
    public string RecipientEmail { get; init; } = string.Empty!;
    public string Subject { get; init; } = string.Empty!;
    public string Body { get; init; } = string.Empty!;
}
