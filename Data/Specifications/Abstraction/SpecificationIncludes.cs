using System.Linq.Expressions;

namespace Data.Specifications.Abstraction;

public class SpecificationIncludes<T, R> : ISpecificationIncludes
{
    public SpecificationIncludes()
    {

    }

    ISpecificationIncludes _thenInclude;

    public string includeString { get; set; }
    public ISpecificationIncludes thenInclude
    {
        get
        {
            return _thenInclude != null ? _thenInclude : null;
        }
    }

    public SpecificationIncludes<R, Next> AddThenInclude<Next>(Expression<Func<R, IEnumerable<Next>>> includeExpression)
    {
        var body = includeExpression.Body.ToString();
        var filteredString = body.Remove(0, body.IndexOf('.') + 1);
        var includeSpec = new SpecificationIncludes<R, Next> { includeString = filteredString };
        _thenInclude = includeSpec;
        return includeSpec;
    }

    //the IEntity here is specified as the clr wont ever call AddThenInclude with IEnumerable
    public SpecificationIncludes<R, Next> AddThenInclude<Next>(Expression<Func<R, Next>> includeExpression)
        where Next : class
    {
        var body = includeExpression.Body.ToString();
        var filteredString = body.Remove(0, body.IndexOf('.') + 1);
        var includeSpec = new SpecificationIncludes<R, Next> { includeString = filteredString };
        _thenInclude = includeSpec;
        return includeSpec;
    }
}

public interface ISpecificationIncludes
{
    public string includeString { get; set; }
    public ISpecificationIncludes thenInclude { get; }
}
