using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace BaseCleanArchitecture.Application.Core.Abstractions.Data;

public interface IDbContext<TKey> : IDbContext where TKey : struct, IEquatable<TKey>
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class, IBaseEntity;

    IQueryable<TEntity> SetAsNotTracking<TEntity>()
        where TEntity : class, IBaseEntity;

    void Add<TEntity>(TEntity entity)
        where TEntity : class, IBaseEntity;

    void AddRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IBaseEntity;

    void SoftDelete<TEntity>(TEntity entity)
        where TEntity : class, IDeletable<TKey>, IBaseEntity;

    void SoftDeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class,IDeletable<TKey>, IBaseEntity;

    void Remove<TEntity>(TEntity entity)
        where TEntity : class, IBaseEntity;

    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity;
}

public interface IDbContext : IUnitOfWork
{
}