using AutoMapper.QueryableExtensions;
using Data.Specifications.Abstraction;

namespace Data.Specifications.Extention
{
    public static class SpecificationQueryableExtention
    {
        //used when there is no criteria used
        public static IQueryable<T> ApplySpecification<T, SpecificationType>(this IQueryable<T> query)
            where SpecificationType : BaseSpecifications<T>, new()
            where T : class
        {
            var spec = new SpecificationType();
            var SpecificationApplicatior = new ApplyQueryableSpecification<T>(spec);
            var specifiedQuery = SpecificationApplicatior.Apply(query);
            return specifiedQuery;

        }

        //specification will be passed as an aggregiation
        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, BaseSpecifications<T> specifications)
           where T : class
        {
            var SpecificationApplicatior = new ApplyQueryableSpecification<T>(specifications);
            var specifiedQuery = SpecificationApplicatior.Apply(query);
            return specifiedQuery;

        }

        //Projects uses automapper projection to project the specification
        public static IQueryable<Projection> ApplySpecificationWithProjection<T, Projection>(this IQueryable<T> query, AutoMapper.IConfigurationProvider config, BaseSpecifications<T> specifications)
           where T : class
        {
            var spec = query.ApplySpecification(specifications);
            var projectedQuery = spec.ProjectTo<Projection>(config);
            return projectedQuery;

        }
    }


    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
