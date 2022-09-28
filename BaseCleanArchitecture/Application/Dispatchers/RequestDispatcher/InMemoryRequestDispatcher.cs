using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCleanArchitecture.Application.Dispatchers.RequestDispatcher;

public sealed class InMemoryRequestDispatcher : IRequestDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryRequestDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
        
    public async Task<TResponse> SendAsync<TRequest,TResponse>(TRequest request,CancellationToken cancellationToken = default) where TRequest : class, IRequest<TResponse>
    {
        var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var pipelines = scope.ServiceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>();
        foreach (var pipeline in pipelines)
        {
            await pipeline.HandleAsync(request,cancellationToken);
        }
      
        return await handler.HandleAsync(request,cancellationToken);
    }
}