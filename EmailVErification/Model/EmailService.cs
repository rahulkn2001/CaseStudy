using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string FromEmail { get; set; }
}

public interface IEmailService
{
    Task SendVerificationEmailAsync(string toEmail, string verificationLink);
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendVerificationEmailAsync(string toEmail, string verificationLink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("MacroKart", _emailSettings.FromEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = "Please verify your email";
        message.Body = new TextPart("plain")
        {
            Text = $"Please verify your email by using OTP: {verificationLink}"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, true);
            await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
