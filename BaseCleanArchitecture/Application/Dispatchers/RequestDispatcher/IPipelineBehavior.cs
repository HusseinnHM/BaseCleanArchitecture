using BaseCleanArchitecture.Application.Core.Abstractions.Request;

namespace BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;


public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}