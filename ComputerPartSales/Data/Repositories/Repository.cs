using Data.Context;
using Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<T> :IRepository<T> where T : class, new()
    {
        private readonly ComputerDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ComputerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);

        }

        public void Delete(T entity)
        {

            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }

        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbSet;

            // Eğer bir filter varsa, onu sorguya uygula
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include işlemi varsa, onu da sorguya uygula
            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties=null)
        {
            /*return await _dbSet.AsNoTracking().ToListAsync();*/ //ef core verileri takip etmiyor (modified,deleted,detached gibi)
            IQueryable<T> query = _dbSet.AsNoTracking(); // Veriler üzerinde izleme yapılmaz (performans artışı)

            if (filter != null)
            {
                query = query.Where(filter); // Eğer bir filtre varsa onu uygula
            }

            return await query.ToListAsync(); // Sorguyu çalıştır ve sonucu döndür
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbSet;

            // Eğer bir filter varsa, onu sorguya uygula
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include işlemi varsa, onu da sorguya uygula
            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }
    }
}
