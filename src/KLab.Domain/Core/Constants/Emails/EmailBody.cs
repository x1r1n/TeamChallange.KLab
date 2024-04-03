namespace KLab.Domain.Core.Constants.Emails
{
	public static class EmailBody
	{
		public static string Verification(string userName, string verificationCode)
		{
			return @$"
                <p>Dear {userName},</p>
                <p>Welcome to KLab! This email serves as confirmation that the first part of the registration has been successfully completed.</p>
                <p>Enter this verification code to complete the registration of your account: {verificationCode}.</p>
                <p>If you have not registered, please disregard this message.</p>
                <br/>
                <p>Best regards,</p>
                <p>KLab Team</p>";
		}

		public static string Authentication(string userName, string authenticationCode)
		{
			return @$"
                <p>Dear {userName},</p>
                <p>Your authentication code: {authenticationCode}.</p>
                <p>If you did not initiate this request, please disregard this message.</p>
                <br/>
                <p>Best regards,</p>
                <p>KLab Team</p>";
		}
	}
}