using Microsoft.Extensions.Configuration;

namespace KLab.Infrastructure.Core.Services.Email
{
	public class SmtpConfiguration
	{
		public string? MailServer { get; init; }
		public string? SenderEmail { get; init; }
		public string? Password { get; init; }
		public int MailPort { get; init; }

		public SmtpConfiguration(IConfiguration configuration)
		{
			MailServer = configuration["EmailSettings:MailServer"];
			SenderEmail = configuration["EmailSettings:SenderEmail"];
			Password = configuration["EmailSettings:Password"];
			MailPort = int.Parse(configuration["EmailSettings:MailPort"]!);
		}
	}
}