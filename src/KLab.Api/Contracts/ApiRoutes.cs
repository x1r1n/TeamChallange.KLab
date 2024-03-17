namespace KLab.Api.Contracts
{
	public static class ApiRoutes
	{
		public static class Authentication
		{
			public const string SignUp = "authentication/sign-up";
			public const string SignIn = "authentication/sign-in";
			public const string VerifyEmail = "authentication/verify-email";
			public const string Authenticate = "authentication/authenticate-email";
			public const string SignOut = "authentication/sign-out";
		}

		public static class Users
		{
			public const string Me = "users/me";
			public const string Update = "users/{id}";
		}
	}
}