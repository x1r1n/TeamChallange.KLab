#nullable disable

namespace KLab.Contracts.Authentication
{
	public class AuthenticateRequest
	{
		public string Email { get; set; }
		public string AuthenticationCode { get; set; }
	}
}