using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Application.Core.Errors
{
	/// <summary>
	/// Represents a class for validation errors
	/// </summary>
	internal static class ValidationErrors
	{
		/// <summary>
		/// Represents a class for user-related errors.
		/// </summary>
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
			internal static Error IdIsRequired => Error.Validition(
				"User.IdIsRequired",
				"The user id is required.");
			internal static Error ImageIsRequired => Error.Validition(
				"User.ImageIsRequired",
				"The image is required.");
			internal static Error ImageContentTypeIsRequired => Error.Validition(
				"User.ImageContentTypeIsRequired",
				"The image content type is required.");
			internal static Error FileMustBeImage => Error.Validition(
				"User.FileMustBeImage",
				"The file must be an image.");
		}

		/// <summary>
		/// Represents a class for authentication-related errors.
		/// </summary>
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