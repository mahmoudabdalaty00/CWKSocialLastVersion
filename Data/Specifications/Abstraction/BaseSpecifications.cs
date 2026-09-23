using System.Linq.Expressions;
using Data.Specifications.Extention;
using Microsoft.IdentityModel.Tokens;

namespace Data.Specifications.Abstraction;

public class BaseSpecifications<T> : ISpecifications<T>
{
    public BaseSpecifications()
    {
        _includes = new List<SpecificationIncludes<T, object>>();
    }
    public BaseSpecifications(Expression<Func<T, bool>> criterial)
    {
        Criterial = criterial;
        _includes = new List<SpecificationIncludes<T, object>>();
    }

    public Expression<Func<T, bool>> Criterial { get; }

    private IEnumerable<ISpecificationIncludes> _includes;

    public IEnumerable<ISpecificationIncludes> Includes
    {
        get
        {
            return !_includes.IsNullOrEmpty() ? _includes : null;
        }
    }

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public Expression<Func<T, int>> ChildCount { get; private set; }

    public List<string> IncludeStrings { get; } = new List<string>();

    protected SpecificationIncludes<T, R> AddInclude<R>(Expression<Func<T, R>> includeExpression)
        where R : new()
    {
        var body = includeExpression.Body.ToString();
        var filteredString = body.Remove(0, body.IndexOf('.') + 1);
        var includeSpec = new SpecificationIncludes<T, R> { includeString = filteredString };
        var list = _includes.ToList();
        list.Add(includeSpec);
        _includes = list;
        return includeSpec;
    }

    protected SpecificationIncludes<T, R> AddInclude<R>(Expression<Func<T, IEnumerable<R>>> includeExpression)
    {
        var body = includeExpression.Body.ToString();
        var filteredString = body.Remove(0, body.IndexOf('.') + 1);
        var includeSpec = new SpecificationIncludes<T, R> { includeString = filteredString };
        var list = _includes.ToList();
        list.Add(includeSpec);
        _includes = list;
        return includeSpec;
    }

    protected void AddChildCount(Expression<Func<T, int>> childCount)
    {
        ChildCount = childCount;
    }


    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderbyDescExpression)
    {
        OrderByDescending = orderbyDescExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}