namespace AutomaticMailSenderPOC.Models;

public class BasicEmail
{
    public string RecipientName { get; set; } = string.Empty!;
    public string RecipientEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
