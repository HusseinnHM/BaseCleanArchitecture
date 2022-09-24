using BaseCleanArchitecture.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCleanArchitecture.Application.Dispatchers.DomainEventDispatcher;

public class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default) where TDomainEvent : IDomainEvent
    {
        using var scope = _serviceProvider.CreateScope();
        
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        await (Task) handlerType
            .GetMethod(nameof(IDomainEventHandler<TDomainEvent>.HandleAsync))?
            .Invoke(handler, new object[] {domainEvent,cancellationToken})!;
    }
}