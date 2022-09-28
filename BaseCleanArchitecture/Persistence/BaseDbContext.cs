using BaseCleanArchitecture.Application.Core.Abstractions.Common;
using BaseCleanArchitecture.Application.Core.Abstractions.Data;
using BaseCleanArchitecture.Application.Dispatchers.DomainEventDispatcher;
using BaseCleanArchitecture.Domain.Events;
using BaseCleanArchitecture.Domain.Primitives;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseCleanArchitecture.Persistence;

public class BaseDbContext<TKey, TUser> :
    BaseDbContext<TKey, TUser, IdentityRole<TKey>, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
        IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>> where TUser : IdentityUser<TKey>
    where TKey : struct, IEquatable<TKey>

{
    public BaseDbContext(DbContextOptions options, IDateTime dateTime, IDomainEventDispatcher domainEventDispatcher) :
        base(options, dateTime, domainEventDispatcher)
    {
    }
}

public class BaseDbContext<TKey> : DbContext, IDbContext<TKey>
    where TKey : struct, IEquatable<TKey>

{
    protected readonly IDateTime DateTime;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BaseDbContext(DbContextOptions options, IDateTime dateTime, IDomainEventDispatcher domainEventDispatcher) :
        base(options)
    {
        DateTime = dateTime;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity
    {
        return base.Set<TEntity>();
    }

    public IQueryable<TEntity> SetAsNotTracking<TEntity>() where TEntity : class, IBaseEntity
    {
        return base.Set<TEntity>().AsNoTracking();
    }

    public new virtual void Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
    {
        base.Add(entity);
    }

    public virtual void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity
    {
        Set<TEntity>().AddRange(entities);
    }

    public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IDeletable<TKey>, IBaseEntity
    {
        entity.DateDeleted = System.DateTime.Now;
        Set<TEntity>().Update(entity);
    }

    public void SoftDeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
        }
    }

    public new virtual void Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
    {
        base.Remove(entity);
    }

    public virtual void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity
    {
        Set<TEntity>().RemoveRange(entities);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(cancellationToken);
        UpdateAuditableEntities(DateTime.Now);
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await Database.BeginTransactionAsync(cancellationToken);

    private void UpdateAuditableEntities(DateTime dateTimeNow)
    {
        foreach (var entityEntry in ChangeTracker.Entries<IBaseEntity<TKey>>())
        {
            switch (entityEntry)
            {
                case { State: EntityState.Added }:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateCreated)).CurrentValue = dateTimeNow;
                    break;
                case { State: EntityState.Modified }
                    when entityEntry.Property(nameof(IBaseEntity<TKey>.DateDeleted)).IsModified:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateDeleted)).CurrentValue = dateTimeNow;
                    break;
                case { State: EntityState.Modified }:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateUpdated)).CurrentValue = dateTimeNow;
                    break;
            }
        }
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        foreach (var domainEntity in ChangeTracker
                     .Entries<AggregateRoot<TKey>>()
                     .Where(a => a.Entity.DomainEvents.Any()))
        {
            foreach (var domainEvent in domainEntity.Entity.DomainEvents)
            {
                await _domainEventDispatcher.PublishAsync(domainEvent, cancellationToken);
            }

            domainEntity.Entity.ClearDomainEvents();
        }
    }
}

public class BaseDbContext<TKey, TUser, TRole, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> :
    IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>,
    IDbContext<TKey>
    where TKey : struct, IEquatable<TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TUserClaim : IdentityUserClaim<TKey>
    where TUserRole : IdentityUserRole<TKey>
    where TUserLogin : IdentityUserLogin<TKey>
    where TRoleClaim : IdentityRoleClaim<TKey>
    where TUserToken : IdentityUserToken<TKey>
{
    protected readonly IDateTime DateTime;

    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BaseDbContext(DbContextOptions options, IDateTime dateTime, IDomainEventDispatcher domainEventDispatcher) :
        base(options)
    {
        DateTime = dateTime;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity
    {
        return base.Set<TEntity>();
    }

    public IQueryable<TEntity> SetAsNotTracking<TEntity>() where TEntity : class, IBaseEntity
    {
        return base.Set<TEntity>().AsNoTracking();
    }

    public new virtual void Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
    {
        base.Add(entity);
    }

    public virtual void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity
    {
        Set<TEntity>().AddRange(entities);
    }

    public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IDeletable<TKey>, IBaseEntity
    {
        entity.DateDeleted = System.DateTime.Now;
        Set<TEntity>().Update(entity);
    }

    public void SoftDeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
        }
    }

    public new virtual void Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
    {
        base.Remove(entity);
    }

    public virtual void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity
    {
        Set<TEntity>().RemoveRange(entities);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(cancellationToken);
        UpdateAuditableEntities(DateTime.Now);
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await Database.BeginTransactionAsync(cancellationToken);

    private void UpdateAuditableEntities(DateTime dateTimeNow)
    {
        foreach (var entityEntry in ChangeTracker.Entries<IBaseEntity<TKey>>())
        {
            switch (entityEntry)
            {
                case { State: EntityState.Added }:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateCreated)).CurrentValue = dateTimeNow;
                    break;
                case { State: EntityState.Modified }
                    when entityEntry.Property(nameof(IBaseEntity<TKey>.DateDeleted)).IsModified:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateDeleted)).CurrentValue = dateTimeNow;
                    break;
                case { State: EntityState.Modified }:
                    entityEntry.Property(nameof(IBaseEntity<TKey>.DateUpdated)).CurrentValue = dateTimeNow;
                    break;
            }
        }
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = new List<IDomainEvent>();
        foreach (var domainEntity in ChangeTracker
                     .Entries<AggregateRoot<TKey>>()
                     .Where(a => a.Entity.DomainEvents.Any()))
        {
            domainEvents.AddRange(domainEntity.Entity.DomainEvents);
            domainEntity.Entity.ClearDomainEvents();
        }

        foreach (var @event in domainEvents)
        {
            await _domainEventDispatcher.PublishAsync(@event, cancellationToken);
        }
    }
}