using KLab.Application.Core.Abstractions.Emails;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace KLab.Infrastructure.Core.Services.Email
{
	/// <summary>
	/// Provides functionality for sending emails.
	/// </summary>
	public class EmailService : IEmailService
	{
		private readonly SmtpConfiguration _smtp;

		public EmailService(SmtpConfiguration smtp)
		{
			_smtp = smtp;
		}

		public async Task SendEmailAsync(string email, string subject, string body)
		{
			var message = ConfigureEmailMessage(_smtp.SenderEmail!, email, subject, body);

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(_smtp.MailServer, _smtp.MailPort, SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(_smtp.SenderEmail, _smtp.Password);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
		}

		private MimeMessage ConfigureEmailMessage(
			string senderEmail,
			string recipientEmail,
			string subject,
			string body)
		{
			var message = new MimeMessage();

			message.From.Add(new MailboxAddress("KLab administration", senderEmail));
			message.To.Add(new MailboxAddress("", recipientEmail));
			message.Subject = subject;
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = body
			};

			return message;
		}
	}
}