namespace KLab.Application.Core.Abstractions.Emails
{
	/// <summary>
	/// Represents the interface for the email service
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Sends an email asynchronously
		/// </summary>
		/// <param name="email">The recipient email address</param>
		/// <param name="subject">The subject of the email</param>
		/// <param name="body">The body content of the email</param>
		/// <returns>A task representing the asynchronous operation</returns>
		Task SendEmailAsync(string email, string subject, string body);
	}
}