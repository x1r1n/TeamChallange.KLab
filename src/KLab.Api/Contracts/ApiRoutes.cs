namespace KLab.Api.Contracts
{
	/// <summary>
	/// Represents the api routes for controller and action
	/// </summary>
	public static class ApiRoutes
	{
		/// <summary>
		/// Represents the api routes for authentication controller
		/// </summary>
		public static class Authentication
		{
			public const string SignUp = "authentication/sign-up";
			public const string SignIn = "authentication/sign-in";
			public const string VerifyEmail = "authentication/verify-email";
			public const string Authenticate = "authentication/authenticate-email";
			public const string SignOut = "authentication/sign-out";
			public const string ResendVerificationCode = "authentication/resend-verification-code";
		}

		/// <summary>
		/// Represents the api routes for users controller
		/// </summary>
		public static class Users
		{
			public const string Me = "users/me";
			public const string User = "users/{id}";
			public const string Image = "users/{id}/image";
		}

		/// <summary>
		/// Represents the api routes for dashboard controller
		/// </summary>
		public static class Dashboard
		{
			public const string UserManagement = "dashboard/user/{id}";
			public const string UserRoleManagement = "dashboard/user/{id}/role";
		}
	}
}