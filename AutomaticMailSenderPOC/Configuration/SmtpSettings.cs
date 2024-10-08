namespace AutomaticMailSenderPOC.Configuration;

public class SmtpSettings
{
    public string Server { get; set; } = string.Empty!;
    public int Port { get; set; }
    public string SenderName { get; set; } = string.Empty!;
    public string SenderEmail { get; set; } = string.Empty!;
    public string Username { get; set; } = string.Empty!;
    public string Password { get; set; } = string.Empty!;
    public string MailTrapUserName { get; set; } = string.Empty!;
    public string MailTrapPassword { get; set; } = string.Empty!;
}
