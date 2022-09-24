using System.Linq.Expressions;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

namespace BaseCleanArchitecture.Application.Specification;

public abstract class Specification<TEntity> : ISpecification<TEntity>  where TEntity : class, IBaseEntity
{
    protected Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    protected Specification()
    {

    }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Expression<Func<TEntity, object>>? GroupBy { get; private set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new ();
    public List<string> IncludeStrings { get; } = new ();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public int? Take { get; private set; }
    public int? Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;

    protected virtual void ApplyFilter(Expression<Func<TEntity, bool>>? filterExpression)
    {
        Criteria = filterExpression;
    }
    protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }

}