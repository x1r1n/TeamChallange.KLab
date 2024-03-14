namespace KLab.Infrastructure.Core.Abstractions
{
    public interface ISecretManager
    {
        Task<string> GetSecretAsync(string secretName);
        Task SetSecretAsync(string secretName, string secretValue);
        Task DeleteSecretAsync(string secretName);
        Task UpdateSecretAsync(string secretName, string secretValue);
    }
}