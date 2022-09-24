using System.Linq.Expressions;
using BaseCleanArchitecture.Application.Core.Abstractions.Data;
using BaseCleanArchitecture.Application.Specification;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;


namespace BaseCleanArchitecture.Domain.Repository;

public interface IRepository
{
}

public interface IRepository<TKey, TEntity> : IRepository
    where TEntity : class, IBaseEntity
    where TKey : struct, IEquatable<TKey>
{
    IUnitOfWork UnitOfWork { get; }
    IQueryable<TEntity> Query<TEntity>() where TEntity : class, IBaseEntity<TKey>;
    IQueryable<TEntity> TrackingQuery<TEntity>() where TEntity : class, IBaseEntity<TKey>;
    Task<List<TEntity>> GetAsync(ISpecification<TEntity> specification);

    Task<List<TResult>> GetAsync<TResult>(ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>> selector);

    Task<TEntity> GetAsync(TKey id, params string[] includeProps);
    Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<TEntity, TResult>> selector, params string[] includeProps);
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includeProps);
    Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector, params string[] includeProps);

    Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> selector, params string[] includeProps);

    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, params string[] includeProps);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, params string[] includeProps);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void SoftDelete<TEntity1>(TEntity1 entity) where TEntity1 : class, IDeletable<TKey>, IBaseEntity;
    void SoftDelete<TEntity1>(IEnumerable<TEntity1> entities) where TEntity1 : class, IDeletable<TKey>, IBaseEntity;

    void SoftDelete<TEntity1, TEntity2>(IEnumerable<TEntity> entities, Func<TEntity, TEntity2> entity2)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity;

    void SoftDelete<TEntity1, TEntity2>(IEnumerable<TEntity> entities, Func<TEntity, IEnumerable<TEntity2>> entity2)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity;

    void SoftDelete<TEntity1, TEntity2, TEntity3>(IEnumerable<TEntity> entities, Func<TEntity, TEntity2> entity2,
        Func<TEntity, TEntity3> entity3)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity
        where TEntity3 : class, IDeletable<TKey>, IBaseEntity;

    void SoftDelete<TEntity1, TEntity2, TEntity3>(IEnumerable<TEntity> entities,
        Func<TEntity, IEnumerable<TEntity2>> entities2,
        Func<TEntity, IEnumerable<TEntity3>> entities3)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity
        where TEntity3 : class, IDeletable<TKey>, IBaseEntity;
}