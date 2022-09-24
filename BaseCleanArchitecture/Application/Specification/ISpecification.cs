using System.Linq.Expressions;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

namespace BaseCleanArchitecture.Application.Specification;

public interface ISpecification<TEntity> where TEntity : class,IBaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public Expression<Func<TEntity, object>>? OrderBy { get; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; }
    public Expression<Func<TEntity, object>>? GroupBy { get; }

    public int? Take { get; }
    public int? Skip { get; }
    public bool IsPagingEnabled { get; }
}