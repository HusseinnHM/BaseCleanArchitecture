using BaseCleanArchitecture.Domain.Events;

namespace BaseCleanArchitecture.Application.Dispatchers.DomainEventDispatcher;

public interface IDomainEventDispatcher
{
    Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : IDomainEvent;

}