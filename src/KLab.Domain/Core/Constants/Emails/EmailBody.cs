namespace KLab.Domain.Core.Constants.Emails
{
	/// <summary>
	/// Provides constants for email body content
	/// </summary>
	public static class EmailBody
	{
		/// <summary>
		/// Generates the email body content for verification
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="verificationCode">The verification code</param>
		/// <returns>The email body content for verification</returns>
		public static string Verification(string username, string verificationCode)
		{
			return @$"
                <p>Dear {username},</p>
                <p>Welcome to KLab! This email serves as confirmation that the first part of the registration has been successfully completed.</p>
                <p>Enter this verification code to complete the registration of your account: {verificationCode}.</p>
                <p>If you have not registered, please disregard this message.</p>
                <br/>
                <p>Best regards,</p>
                <p>KLab Team</p>";
		}

		/// <summary>
		/// Generates the email body content for authentication
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="authenticationCode">The authentication code</param>
		/// <returns>The email body content for authentication</returns>
		public static string Authentication(string username, string authenticationCode)
		{
			return @$"
                <p>Dear {username},</p>
                <p>Your authentication code: {authenticationCode}.</p>
                <p>If you did not initiate this request, please disregard this message.</p>
                <br/>
                <p>Best regards,</p>
                <p>KLab Team</p>";
		}
	}
}