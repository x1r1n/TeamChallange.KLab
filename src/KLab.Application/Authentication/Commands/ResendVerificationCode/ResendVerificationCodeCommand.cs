using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.ResendVerificationCode
{
	/// <summary>
	/// Represents a command for resend verification code
	/// </summary>
	public class ResendVerificationCodeCommand : ICommand<Result>
	{
		public string Email { get; init; }

		public ResendVerificationCodeCommand(string email)
		{
			Email = email;
		}
	}
}
