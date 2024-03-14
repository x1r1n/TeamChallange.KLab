using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Domain.Core.Errors
{
    public static class DomainErrors
	{
		public static class Server
		{
			public static Error InternalError => Error.Failure(
				"Server.InternalError",
				"An internal error occurred on the server");
		}

		public static class User
		{
			public static Error NotFound => Error.NotFound(
				"User.NotFound",
				"The user with this email address is not yet registered.");

			public static Error AlreadyRegistered => Error.Conflict(
				"User.AlreadyRegistered",
				"The user with this email has already been registered.");
		}

		public static class Authentication
		{
			public static Error UserNotFound => Error.NotFound(
				"Authentication.UserNotFound",
				"The user with this email address is not yet registered.");

			public static Error EmailAlreadyVerified => Error.Conflict(
				"Authentication.EmailAlreadyVerified",
				"The email has already been verified.");

			public static Error UnverifiedEmail => Error.Failure(
				"Authentication.UnverifiedEmail",
				"The email is not yet verified.");

			public static Error IncorrectVerificationCode => Error.Failure(
				"Authentication.IncorrectVerificationCode",
				"The verification code is incorrect.");

			public static Error IncorrectAuthenticationCode => Error.Failure(
				"Authentication.IncorrectAuthenticationCode",
				"The authentication code is incorrect.");
		}

		public static class ClientAuthentication
		{
			public static Error MissingApiKey => Error.Failure(
				"ClientAuthentication.MissingApiKey",
				"The API Key is missing.");

			public static Error UninitializedApiKey => Error.Failure(
				"ClientAuthentication.UninitializedApiKey",
				"The API Key is uninitialized.");

			public static Error IncorrectApiKey => Error.Failure(
				"ClientAuthentication.IncorrectApiKey",
				"The API Key is incorrect.");
		}
	}
}