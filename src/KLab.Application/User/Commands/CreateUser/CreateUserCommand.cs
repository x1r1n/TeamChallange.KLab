using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

#nullable disable

namespace KLab.Application.User.Commands.CreateUser
{
	/// <summary>
	/// Represents a command for creating a user.
	/// </summary>
	public class CreateUserCommand : ICommand<Result>
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