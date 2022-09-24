using BaseCleanArchitecture.Application.Core.Abstractions.Request;

namespace BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;

public interface IRequestDispatcher
{
    Task<TResponse> SendAsync<TRequest,TResponse>(TRequest request,CancellationToken cancellationToken = default) where TRequest : class, IRequest<TResponse>;
}