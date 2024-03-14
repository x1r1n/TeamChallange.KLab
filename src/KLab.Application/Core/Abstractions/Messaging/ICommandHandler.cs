using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
	}
}