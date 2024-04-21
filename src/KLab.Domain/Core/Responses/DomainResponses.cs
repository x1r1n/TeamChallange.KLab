namespace KLab.Domain.Core.Responses
{
	/// <summary>
	/// Provides constants for domain responses
	/// </summary>
	public static class DomainResponses
	{
		/// <summary>
		/// Provides responses related to email verification
		/// </summary>
		public static class Email
		{
			public static string VerificationCodeSent => "The verification code has been sent to your email.";
			public static string AuthenticationCodeSent => "The authentication code has been sent to your email.";
		}
	}
}
