namespace BaseCleanArchitecture.Application.Core.Abstractions.Request;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest query,CancellationToken cancellationToken = default);
}