#nullable disable

namespace KLab.Contracts.Authentication
{
	public class VerifyEmailRequest
	{
		public string Email { get; set; }
		public string VerificationCode { get; set; }
	}
}