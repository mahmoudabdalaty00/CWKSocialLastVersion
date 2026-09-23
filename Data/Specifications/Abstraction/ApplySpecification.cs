using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Data.Specifications.Extention;
namespace Data.Specifications.Abstraction
{
    public class ApplyQueryableSpecification<T> : ISpecificationApplicator<T>
        where T : class
    {
        private readonly BaseSpecifications<T> _spec;
        public ApplyQueryableSpecification(BaseSpecifications<T> spec)
        {
            _spec = spec;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            IQueryable<T> specifiedQuery = query;

            if (_spec.Criterial != null)
                specifiedQuery = specifiedQuery.Where(_spec.Criterial);



            //includes
            if (!_spec.Includes.IsNullOrEmpty())
            {
                foreach (var item in _spec.Includes)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(item.includeString);
                    if (item.thenInclude != null)
                        AppendSpecIncludes(stringBuilder, item.thenInclude);

                    specifiedQuery = specifiedQuery.Include(stringBuilder.ToString());
                }
            }

            //string includes
            if (!_spec.IncludeStrings.IsNullOrEmpty())
            {
                foreach (var item in _spec.IncludeStrings)
                {
                    specifiedQuery = specifiedQuery.Include(item);
                }
            }

            if (_spec.OrderBy != null)
                specifiedQuery = specifiedQuery.OrderBy(_spec.OrderBy);

            if (_spec.OrderByDescending != null)
                specifiedQuery = specifiedQuery.OrderBy(_spec.OrderBy);

            if (_spec.IsPagingEnabled)
            {
                specifiedQuery = specifiedQuery.Skip(_spec.Skip).Take(_spec.Take);
            }

            if (_spec.ChildCount != null)
            {
                //unknown use
            }

            return specifiedQuery;
        }

        //output ex: "include.thenInclude"
        private void AppendSpecIncludes(StringBuilder stringBuilder, ISpecificationIncludes include)
        {
            stringBuilder.Append($".{include.includeString}");
            if (include.thenInclude != null)
            {
                AppendSpecIncludes(stringBuilder, include.thenInclude);
            }
        }
    }
}
