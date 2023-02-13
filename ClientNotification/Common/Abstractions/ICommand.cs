using MediatR;

namespace ClientNotification.Common.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseRequest
    {
    }
}
