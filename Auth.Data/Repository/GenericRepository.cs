using Auth.Core.Repository;
using Auth.Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Auth.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
     private readonly  DbContext _context;
     private DbSet<T> _table;


        public GenericRepository(AppDbContext db)
        {
          
            _context = db;
            _table = db.Set<T>();
            

        }

        public async Task AddAsync(T entity)
        {
          await _table.AddAsync(entity);
         
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
          var entity=await _table.FindAsync(id);
            if(entity!=null)
            {
                _context.Entry(entity).State=EntityState.Detached;
            }
            return entity;

        }

        public void Remove(T entity)
        {
            _table.Remove(entity);
        }

        public T Update(T entity)
        {
            _table.Update(entity);
            return entity;
        }

        public  IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return  _table.Where(predicate);
        }
    }
    
    }

