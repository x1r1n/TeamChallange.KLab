using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.SignIn
{
	public class SignInCommand : ICommand<Result<object>>
	{
		public string? Email { get; init; }

		public SignInCommand(string email) => Email = email;
	}
}