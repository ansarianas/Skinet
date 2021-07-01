using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Specifications;


namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            var criteria = spec.Criteria;

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}