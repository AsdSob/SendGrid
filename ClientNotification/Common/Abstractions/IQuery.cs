using MediatR;

namespace ClientNotification.Common.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>, IBaseRequest
    {
    }
}
