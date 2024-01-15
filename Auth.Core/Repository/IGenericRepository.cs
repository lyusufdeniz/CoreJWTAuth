using System.Linq.Expressions;

namespace Auth.Core.Repository
{
    public interface IGenericRepository<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> GetAllAsync();
        Task<Tentity> GetByIDAsync(int id);
        Task<IQueryable<Tentity>> Where(Expression<Func<Tentity, bool>> predicate);
        Task AddAsync(Tentity entity);
        void Remove(Tentity entity);
        Tentity Update(Tentity entity);



    }
}
