using System.Linq.Expressions;

namespace Auth.Core.Repository
{
    public interface IGenericRepository<Tentity> where Tentity : class
    {
        IEnumerable<Tentity> GetAllAsync();
        Task<Tentity> GetByIDAsync(int id);
        IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate);
        Task AddAsync(Tentity entity);
        void Remove(Tentity entity);
        Tentity Update(Tentity entity);



    }
}
