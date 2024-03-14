using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KLab.Infrastructure.Core.Abstractions;

namespace KLab.Api.Infrastructure.Services
{
    public class SecretManager : ISecretManager
    {
        private readonly SecretClient _secretClient;

        public SecretManager(IConfiguration configuration)
        {
            string keyVaultUrl = configuration["KeyVaultSettings:KeyVaultUrl"]!;

            TokenCredential credential = new ClientSecretCredential(
                configuration["KeyVaultSettings:TenantId"],
                configuration["KeyVaultSettings:ClientId"],
                configuration["KeyVaultSettings:ClientSecret"]);

            _secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);

            return secret is not null ? secret.Value : string.Empty;
        }

        public async Task SetSecretAsync(string secretName, string secretValue)
        {
            await _secretClient.SetSecretAsync(secretName, secretValue);
        }

        public async Task UpdateSecretAsync(string secretName, string secretValue)
        {
            await SetSecretAsync(secretName, secretValue);
        }

        public async Task DeleteSecretAsync(string secretName)
        {
            await _secretClient.StartDeleteSecretAsync(secretName);
        }
    }
}
