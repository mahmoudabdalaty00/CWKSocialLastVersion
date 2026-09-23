namespace Data.Specifications.Abstraction;

public interface ISpecificationApplicator<T>
{
    public IQueryable<T> Apply(IQueryable<T> query);
}