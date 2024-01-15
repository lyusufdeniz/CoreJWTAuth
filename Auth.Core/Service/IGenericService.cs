using SharedLibrary.DTO;
using System.Linq.Expressions;

namespace Auth.Core.Service
{
    public interface IGenericService<Tentity,TDTO> where Tentity : class where TDTO : class

    {

        Task<IEnumerable<ResponseDTO<TDTO>>> GetAllAsync();
        Task<ResponseDTO<TDTO>> GetByIDAsync(int id);
        Task<ResponseDTO<IEnumerable<TDTO>>> Where(Expression<Func<Tentity, bool>> predicate);
        Task<ResponseDTO<TDTO>> AddAsync(TDTO entity);
        Task<ResponseDTO<NoContentDTO>> Remove(TDTO entity);
        Task<ResponseDTO<NoContentDTO>> Update(TDTO entity);


    }
}
