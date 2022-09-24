using System.Linq.Expressions;
using BaseCleanArchitecture.Application.Core.Abstractions.Data;
using BaseCleanArchitecture.Application.Specification;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace BaseCleanArchitecture.Domain.Repository;

public abstract class Repository<TKey, TEntity, TContext> : IRepository<TKey, TEntity>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IBaseEntity<TKey>
    where TContext : IDbContext<TKey>, IUnitOfWork
{
    private readonly TContext _context;

    public Repository(TContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IBaseEntity<TKey>
    {
        return _context.SetAsNotTracking<TEntity>();
    }

    public IQueryable<TEntity> TrackingQuery<TEntity>() where TEntity : class, IBaseEntity<TKey>
    {
        return _context.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAsync(ISpecification<TEntity> specification)
    {
        return await ApplySpecification(specification);
    }

    public async Task<List<TResult>> GetAsync<TResult>(ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>> selector)
    {
        return await ApplySpecification(specification, selector);
    }

    public async Task<TEntity> GetAsync(TKey id, params string[] includeProps)
    {
        return await GetFirstAsync(entity => entity.Id.Equals(id), includeProps);
    }

    public async Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<TEntity, TResult>> selector,
        params string[] includeProps)
    {
        return (await GetAsync(e => e.Id.Equals(id), selector, includeProps)).First()!;
    }

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includeProps)
    {
        return await Include(includeProps).Where(filter).ToListAsync();
    }

    public async Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        params string[] includeProps)
    {
        return await Include(includeProps).Select(selector).ToListAsync();
    }

    public async Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> selector, params string[] includeProps)
    {
        return await Include(includeProps).Where(filter).Select(selector).ToListAsync();
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, params string[] includeProps)
    {
        return (await GetAsync(filter, includeProps)).First();
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        params string[] includeProps)
    {
        return (await GetAsync(filter, includeProps)).FirstOrDefault();
    }


    public void Add(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void SoftDelete<TEntity1>(TEntity1 entity) where TEntity1 : class, IDeletable<TKey>, IBaseEntity
    {
        _context.SoftDelete(entity);
    }

    public void SoftDelete<TEntity1>(IEnumerable<TEntity1> entities)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
        }
    }

    public void SoftDelete<TEntity1, TEntity2>(IEnumerable<TEntity> entities, Func<TEntity, TEntity2> entity2)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity where TEntity2 : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
            SoftDelete(entity2(entity));
        }
    }

    public void SoftDelete<TEntity1, TEntity2>(IEnumerable<TEntity> entities,
        Func<TEntity, IEnumerable<TEntity2>> entity2) where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
            SoftDelete(entity2(entity));
        }
    }

    public void SoftDelete<TEntity1, TEntity2, TEntity3>(IEnumerable<TEntity> entities, Func<TEntity, TEntity2> entity2,
        Func<TEntity, TEntity3> entity3)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity
        where TEntity3 : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
            SoftDelete(entity2(entity));
            SoftDelete(entity3(entity));
        }
    }

    public void SoftDelete<TEntity1, TEntity2, TEntity3>(IEnumerable<TEntity> entities,
        Func<TEntity, IEnumerable<TEntity2>> entities2, Func<TEntity, IEnumerable<TEntity3>> entities3)
        where TEntity1 : class, IDeletable<TKey>, IBaseEntity
        where TEntity2 : class, IDeletable<TKey>, IBaseEntity
        where TEntity3 : class, IDeletable<TKey>, IBaseEntity
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
            SoftDelete(entities2(entity));
            SoftDelete(entities3(entity));
        }
    }


    private IQueryable<TEntity> Include(params string[] includeProps)
    {
        return includeProps.Aggregate(_context.Set<TEntity>().AsQueryable(),
            (query, includeProp) => query.Include(includeProp));
    }

    private async Task<List<TResult>> ApplySpecification<TResult>(ISpecification<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector)
    {
        return await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), spec).Select(selector)
            .ToListAsync();
    }

    private async Task<List<TEntity>> ApplySpecification(ISpecification<TEntity> spec)
    {
        return await SpecificationEvaluator<TEntity>.GetList(_context.Set<TEntity>(), spec);
    }
}