using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

#nullable disable

namespace KLab.Application.User.Commands.CreateUser
{
	public class CreateUserCommand : ICommand<Result<object>>
	{
		public string UserName { get; init; }
		public string Email { get; init; }

		public CreateUserCommand(string userName, string email)
		{
			UserName = userName;
			Email = email;
		}
	}
}