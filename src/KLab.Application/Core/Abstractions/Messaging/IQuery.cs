using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	/// <summary>
	/// Represents a query with a response of type TResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of response</typeparam>
	public interface IQuery<out TResponse> : IRequest<TResponse>
	{
	}
}