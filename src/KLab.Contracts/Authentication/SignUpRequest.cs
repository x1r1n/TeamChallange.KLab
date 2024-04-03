#nullable disable

using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Authentication
{
	/// <summary>
	/// Represents a request for signing up
	/// </summary>
	public class SignUpRequest
	{
		[Required]
		public string Username { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}