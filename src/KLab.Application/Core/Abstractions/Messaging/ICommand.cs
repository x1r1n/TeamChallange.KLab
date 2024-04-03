using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	/// <summary>
	/// Represents a command with a response of type TResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of response</typeparam>
	public interface ICommand<out TResponse> : IRequest<TResponse>
	{
	}
}