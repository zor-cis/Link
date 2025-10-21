using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Domain.Interfaces;

namespace LinkUp.Core.Applicacion.Services
{
    public class GenericService<Entity, Dto> : IGenericService<Dto> where Entity : class where Dto : class
    {
        private readonly IGenericRepository<Entity> _repo;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<ResponseDto<Dto>?> AddAsync(Dto dto)
        {
            var response = new ResponseDto<Dto>();
            try
            {

                Entity entity = _mapper.Map<Entity>(dto);
                var ReturnEntity = await _repo.AddAsync(entity);

                if (ReturnEntity == null)
                {
                    response.IsError = true;
                    response.MessageResult = "No se pudo completar la accion solicitada";
                    return response;
                }

                response.IsError = false;
                response.Result = _mapper.Map<Dto>(ReturnEntity);

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public virtual async Task<ResponseDto<Dto>?> EditAsync(Dto dto, int Id)
        {
            var response = new ResponseDto<Dto>();
            try
            {

                var entity = await _repo.GetById(Id);
                if (entity == null)
                {
                    response.IsError = true;
                    response.MessageResult = "No se puede completar el proceso";
                    return response;
                }

                _mapper.Map(dto, entity);
                var returnResult = await _repo.UpdateAsync(Id, entity);

                if (returnResult == null)
                {
                    response.IsError = true;
                    response.MessageResult = "Error al actualizar";
                    return response;
                }

                response.IsError = false;
                response.Result = _mapper.Map<Dto>(returnResult);
                response.MessageResult = "Actualizacion existosa";

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public virtual async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var entity = await _repo.GetById(Id);

                if (entity == null)
                {
                    return false;
                }

                await _repo.DeleteAsync(Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<ResponseDto<Dto>?> GetByIdAsync(int Id)
        {
            var response = new ResponseDto<Dto>();
            try
            {
                var entity = await _repo.GetById(Id);
                if (entity == null)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontro la solicutud";
                    return response;
                }

                response.IsError = false;
                response.Result = _mapper.Map<Dto>(entity);

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public virtual async Task<ResponseDto<List<Dto>>?> GetAllAsync()
        {
            var response = new ResponseDto<List<Dto>>();
            try
            {
                var entities = await _repo.GetAllList();
                if (entities == null)
                {
                    response.IsError = true;
                    response.MessageResult = "Error al listar";
                    return response;
                }

                response.IsError = false;
                response.Result = _mapper.Map<List<Dto>>(entities);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }
    }
}
