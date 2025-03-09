using MediatR;

namespace UniqueShit.Application.Core.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
