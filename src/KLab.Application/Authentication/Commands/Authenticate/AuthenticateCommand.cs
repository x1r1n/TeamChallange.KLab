using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.Authenticate
{
	public class AuthenticateCommand : ICommand<Result>
	{
		public string? Email { get; set; }
		public string? AuthenticationCode { get; set; }

		public AuthenticateCommand(string email, string authenticationCode)
		{
			Email = email;
			AuthenticationCode = authenticationCode;
		}
	}
}