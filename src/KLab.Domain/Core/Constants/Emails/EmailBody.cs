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
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Повідомлення про реєстрацію</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }}
        .wrapper {{
            max-width: 600px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border: 1px solid rgba(0, 0, 0, 0.2);
        }}
        h1 {{
            color: #8eaccd;
            text-align: center;
            margin-bottom: 20px;
        }}
        p {{
            color: #333333;
            margin-bottom: 20px;
            line-height: 1.6;
            text-align: center;
            font-size: 17px;
        }}
        .verification-code {{
            font-size: 24px;
            font-weight: bold;
            text-align: center;
            background-color: #f9f3cc; 
            color: #000000;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 30px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }}
        .footer {{
            text-align: center;
            margin-top: 20px;
            color: #8eaccd;
            font-weight: bold;
        }}
    </style>
</head>
<body>
    <div class=""wrapper"">
        <h1>Завершення реєстрації</h1>
        <p>Ласкаво просимо до нашого сервісу, {username}!</p>
        <p>Ваш обліковий запис успішно зареєстровано.</p>
        <p>Для завершення реєстрації введіть наступний код верифікації:</p>
        <div class=""verification-code"">{verificationCode}</div>
        <p>Якщо ви не реєструвалися, проігноруйте це повідомлення.</p>
        <div class=""footer"">
            <p>З найкращими побажаннями від команди КЛаб!</p>
        </div>
    </div>
</body>
</html>

";
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
<!DOCTYPE html>
<html lang=""uk"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Повідомлення про реєстрацію</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }}
        .wrapper {{
            border: 1px solid rgba(142, 172, 205, 0.2);
            border-radius: 5px;
            max-width: 600px;
            margin: 50px auto;
            padding: 30px;
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
        }}
        h1 {{
            color: #8eaccd;
            text-align: center;
            margin-bottom: 20px;
        }}
        p {{
            color: #333333;
            margin-bottom: 20px;
            line-height: 1.6;
            text-align: center;
            font-size: 17px;
        }}
        .authentication-code {{
            font-size: 24px;
            font-weight: bold;
            text-align: center;
            background-color: #f9f3cc;
            color: #000000;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 30px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }}
        .footer {{
            text-align: center;
            margin-top: 20px;
            color: #8eaccd;
            font-weight: bold;
        }}
    </style>
</head>
<body>
    <div class=""wrapper"">
        <h1>Автентифікація</h1>
        <p>Вітаємо, {username}!</p>
        <p>Ваш код автентифікації:</p>
        <div class=""authentication-code"">{authenticationCode}.</div>
        <p>Якщо ви не планували автентифікуватися, проігноруйте це повідомлення.</p>
        <div class=""footer"">
            <p>З найкращими побажаннями від команди КЛаб!</p>
        </div>
    </div>
</body>
</html>
";
		}
	}
}