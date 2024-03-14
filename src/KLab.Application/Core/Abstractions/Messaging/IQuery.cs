using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	public interface IQuery<out TResponse> : IRequest<TResponse>
	{
	}
}