using KLab.Domain.Entities;

namespace KLab.Application.Core.Abstractions.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}