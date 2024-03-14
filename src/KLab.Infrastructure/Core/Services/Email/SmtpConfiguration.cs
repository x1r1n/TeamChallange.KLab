using KLab.Infrastructure.Core.Abstractions;

namespace KLab.Infrastructure.Core.Services.Email
{
    public class SmtpConfiguration
    {
        private readonly ISecretManager _secretManager;

        public string? MailServer { get; private set; }
        public string? SenderEmail { get; private set; }
        public string? Password { get; private set; }
        public int MailPort { get; private set; }

        public SmtpConfiguration(ISecretManager secretManager)
        {
            _secretManager = secretManager;
		}

        public async Task ConfigureSmtp()
        {
            MailServer = await _secretManager.GetSecretAsync(nameof(MailServer));
            SenderEmail = await _secretManager.GetSecretAsync(nameof(SenderEmail));
            Password = await _secretManager.GetSecretAsync(nameof(Password));
            MailPort = int.Parse(await _secretManager.GetSecretAsync(nameof(MailPort)));
        }
    }
}