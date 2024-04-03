#nullable disable

using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Authentication
{
	/// <summary>
	/// Represents a request for email verification
	/// </summary>
	public class VerifyEmailRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string VerificationCode { get; set; }
	}
}