using System.Linq.Expressions;

namespace Data.Specifications.Abstraction;

public interface ISpecifications<T>
{
    Expression<Func<T, bool>> Criterial { get; }
    public IEnumerable<ISpecificationIncludes> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    List<string> IncludeStrings { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
