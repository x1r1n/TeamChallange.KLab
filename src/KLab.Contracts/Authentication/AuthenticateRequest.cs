#nullable disable

using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Authentication
{
	/// <summary>
	/// Represents a request for authentication
	/// </summary>
	public class AuthenticateRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string AuthenticationCode { get; set; }
	}
}