using BaseCleanArchitecture.Domain.Events;
using BaseCleanArchitecture.Domain.Primitives.Entity.Base;

namespace BaseCleanArchitecture.Domain.Primitives;

public abstract class AggregateRoot<TIndex> : BaseEntity<TIndex> where TIndex : struct, IEquatable<TIndex>
{
    private readonly List<IDomainEvent> _domainEvents = new ();
        
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();
        
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}