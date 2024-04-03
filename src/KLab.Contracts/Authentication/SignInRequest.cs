#nullable disable

using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Authentication
{
	/// <summary>
	/// Represents a request for signing in
	/// </summary>
	public class SignInRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}