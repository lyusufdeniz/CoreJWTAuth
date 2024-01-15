using Auth.Core.Repository;
using Auth.Core.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
     private readonly  AppDbContext db;
     private DbSet<T> Table;


        public GenericRepository(AppDbContext db, DbSet<T> table)
        {
            this.db = db;
            Table = db.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
          await  db.AddAsync(entity);
           
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
          var entity=await Table.FindAsync(id);
            if(entity!=null)
            {
                db.Entry(entity).State=EntityState.Detached;
            }
            return entity;

        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

        public T Update(T entity)
        {
            Table.Update(entity);
            return entity;
        }

        public async Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return  Table.Where(predicate);
        }
    }
    
    }

