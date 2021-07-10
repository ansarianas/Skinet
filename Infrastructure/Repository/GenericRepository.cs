using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Core.Specifications;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<List<T>> GetListAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec)
        {
            var entitiesWithSpec = await ApplySpecification(spec).ToListAsync();
            return entitiesWithSpec;
        }

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            var entityWithSpec = await ApplySpecification(spec).FirstOrDefaultAsync();
            return entityWithSpec;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var filteredQuery = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            return filteredQuery;
        }
    }
}