using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Authentication
{
	public class ResendVerificationCodeRequest
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
	}
}
