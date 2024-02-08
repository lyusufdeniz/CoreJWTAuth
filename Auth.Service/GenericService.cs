using Auth.Core.Repository;
using Auth.Core.Service;
using AutoMapper;
using SharedLibrary.DTO;
using System.Linq.Expressions;

namespace Auth.Service
{
    public class GenericService<Tentity, TDTO> : IGenericService<Tentity, TDTO> where Tentity : class where TDTO : class
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IGenericRepository<Tentity> _repo;
        private readonly IMapper _mapper;



        public GenericService(IUnitOfWork unitofWork, IGenericRepository<Tentity> repo, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<TDTO>> AddAsync(TDTO entity)
        {
            var data = _mapper.Map<Tentity>(entity);
            await _repo.AddAsync(data);
            await _unitofWork.CommitAsync();
            return ResponseDTO<TDTO>.Succes(200);
        }

        public async Task<ResponseDTO<IEnumerable<TDTO>>> GetAllAsync()
        {
            var mapped = _mapper.Map<IEnumerable<TDTO>>(await _repo.GetAllAsync());
            return ResponseDTO<IEnumerable<TDTO>>.Succes(mapped, 200);
        }

        public async Task<ResponseDTO<TDTO>> GetByIDAsync(int id)
        {
            var entity = await _repo.GetByIDAsync(id);
            if (entity == null)
            {
                return ResponseDTO<TDTO>.Fail(404, new ErrorDTO("NOT fOUND", true));
            }
            else
            {
                var mapped = _mapper.Map<TDTO>(entity);
                return ResponseDTO<TDTO>.Succes(mapped, 200);
            }


        }

        public async Task<ResponseDTO<NoContentDTO>> Remove(int id)
        {
            var entity = await _repo.GetByIDAsync(id);
            if (entity == null)
            {
                return ResponseDTO<NoContentDTO>.Fail(404, "NOT FOUND", true);
            }
            else
            {
                var mapped = _mapper.Map<Tentity>(entity);
                _repo.Remove(mapped);
                await _unitofWork.CommitAsync();
                return ResponseDTO<NoContentDTO>.Succes(204);
            }


        }

        public async Task<ResponseDTO<NoContentDTO>> Update(int id, TDTO data)
        {
            var entity = await _repo.GetByIDAsync(id);
            if (entity == null)
            {
                return ResponseDTO<NoContentDTO>.Fail(404, "NOT FOUND", true);
            }
            else
            {
                var mapped = _mapper.Map<Tentity>(data);
                _repo.Update(mapped);
                await _unitofWork.CommitAsync();
            }


            return ResponseDTO<NoContentDTO>.Succes(204);
        }

        public async Task<ResponseDTO<IEnumerable<TDTO>>> Where(Expression<Func<Tentity, bool>> predicate)
        {
            var entity = _repo.Where(predicate);
            var mapped = _mapper.Map<IEnumerable<TDTO>>(entity);
            return ResponseDTO<IEnumerable<TDTO>>.Succes(mapped, 200);


        }
    }
}
