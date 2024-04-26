using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.DeleteUser
{
	/// <summary>
	/// Represents a command for handler to delete user
	/// </summary>
	public class DeleteUserCommand : ICommand<Result>
	{
		public string Id { get; init; }

		public DeleteUserCommand(string id)
		{
			Id = id;
		}
	}
}
