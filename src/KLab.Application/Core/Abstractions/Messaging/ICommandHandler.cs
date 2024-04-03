using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	/// <summary>
	/// Represents a handler for a command with a response of type TResponse
	/// </summary>
	/// <typeparam name="TCommand">The type of command</typeparam>
	/// <typeparam name="TResponse">The type of response</typeparam>
	public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
	}
}