using MediatR;

namespace UniqueShit.Application.Core.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
