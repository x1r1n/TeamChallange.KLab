#nullable disable

namespace KLab.Contracts.Authentication
{
	public class SignUpRequest
	{
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}