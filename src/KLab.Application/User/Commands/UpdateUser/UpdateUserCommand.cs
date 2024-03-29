using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.UpdateUser
{
	public class UpdateUserCommand : ICommand<Result>
	{
		public string Id { get; init; }
		public string Nickname { get; init; }
		public string Description { get; init; }

		public UpdateUserCommand(string id, string nickname, string description)
		{
			Id = id;
			Nickname = nickname;
			Description = description;
		}
	}
}
