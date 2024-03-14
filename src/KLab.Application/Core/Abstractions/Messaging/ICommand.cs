using MediatR;

namespace KLab.Application.Core.Abstractions.Messaging
{
	public interface ICommand<out TResponse> : IRequest<TResponse>
	{
	}
}