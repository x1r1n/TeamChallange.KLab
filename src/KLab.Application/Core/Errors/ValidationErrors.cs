using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Application.Core.Errors
{
	internal class ValidationErrors
	{
		internal static class User
		{
			internal static Error UserNameIsRequired => Error.Validition(
				"User.UserNameIsRequired",
				"The username is required.");
			internal static Error UserNameIsNotValid => Error.Validition(
				"User.UserNameIsNotValid",
				"The username is not valid.");
			internal static Error EmailIsRequired => Error.Validition(
				"User.EmailIsRequired",
				"The email is required.");
			internal static Error EmailIsNotValid => Error.Validition(
				"User.EmailIsNotValid",
				"The email is not valid.");
			internal static Error UserIdIsRequired => Error.Validition(
				"User.UserIdIsRequired",
				"THe user id is required.");
		}

		internal static class Authentication
		{
			internal static Error EmailIsRequired => Error.Validition(
				"Authentication.EmailIsRequired",
				"The username is required.");

			internal static Error EmailIsNotValid => Error.Validition(
				"Authentication.EmailIsNotValid",
				"The email is not valid.");

			internal static Error VerificationCodeIsRequired => Error.Validition(
				"Authentication.VerificationCodeIsRequired",
				"The verification code is required.");

			internal static Error AuthenticationCodeIsRequired => Error.Validition(
				"Authentication.AuthenticationCodeIsRequired",
				"The authentication code is required.");
		}
	}
}