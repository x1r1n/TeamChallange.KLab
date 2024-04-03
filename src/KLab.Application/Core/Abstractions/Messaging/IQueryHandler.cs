using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	/// <summary>
	/// Represents a handler for a query with a response of type TResponse
	/// </summary>
	/// <typeparam name="TQuery">The type of query</typeparam>
	/// <typeparam name="TResponse">The type of response</typeparam>
	public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
		where TQuery : IQuery<TResponse>
	{
	}
}