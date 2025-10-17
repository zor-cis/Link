using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly LinkUpContext _context;

        public GenericRepository(LinkUpContext context)
        {
            _context = context;
        }

        //Add
        public virtual async Task<Entity?> AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //Update
        public virtual async Task<Entity?> UpdateAsync(int Id, Entity entity)
        {
            var entry = await _context.Set<Entity>().FindAsync(Id);
            if (entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        //Delete
        public virtual async Task DeleteAsync(int Id)
        {
            var entity = await _context.Set<Entity>().FindAsync(Id);

            if (entity != null)
            {
                _context.Set<Entity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        //GetAll
        public virtual async Task<List<Entity>?> GetAllList()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        //GetAllIncluide (List)
        public virtual async Task<List<Entity>?> GetAllListIncluide(List<string> properties)
        {
            var query = _context.Set<Entity>().AsQueryable();
            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return await query.ToListAsync();
        }

        //GetById
        public virtual async Task<Entity?> GetById(int Id)
        {
            return await _context.Set<Entity>().FindAsync(Id);
        }

        //GetAllQuery
        public virtual IQueryable<Entity> GetQuery()
        {
            return _context.Set<Entity>().AsQueryable();
        }

        //GetQueryIncluide
        public virtual IQueryable<Entity> GetQueryWithIncluide(List<string> properties)
        {
            var query = _context.Set<Entity>().AsQueryable();
            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query;
        }
    }
}
