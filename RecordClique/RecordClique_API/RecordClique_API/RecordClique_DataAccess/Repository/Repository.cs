using RecordClique_DataAccess.Context;
using RecordClique_DataAccess.Repository.Abstraction;
using System.Linq.Expressions;

namespace RecordClique_DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly RecordCliqueContext _context;
        public Repository(RecordCliqueContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity item, bool applyChanges = true)
        {
            _context.Set<TEntity>().Add(item);
            if (applyChanges)
            {
                await SaveChangesAsync();
            }
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync<TId>(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity updatedItem, Guid id, bool applyChanges = true)
        {
            var existingItem = await _context.Set<TEntity>().FindAsync(id);

            if (existingItem != null && applyChanges)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(updatedItem);
                await SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(TEntity item, bool applyChanges = true)
        {
            _context.Set<TEntity>().Remove(item);
            if (applyChanges)
            {
                await SaveChangesAsync();
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> items, bool applyChanges = true)
        {
            _context.Set<TEntity>().RemoveRange(items);
            if (applyChanges)
            {
                await SaveChangesAsync();
            }
        }

        protected Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
