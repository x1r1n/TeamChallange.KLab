using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.DeleteUserImage
{
	/// <summary>
	/// Represents a command for deleting a user's image
	/// </summary>
	public class DeleteUserImageCommand : ICommand<Result>
	{
		public string Id { get; init; }

		public DeleteUserImageCommand(string id)
		{
			Id = id;
		}
	}
}
