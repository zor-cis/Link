using LinkUp.Core.Applicacion.Dtos.Response;

namespace LinkUp.Core.Applicacion.Services
{
    public interface IGenericService<Dto> where Dto : class
    {
        Task<ResponseDto<Dto>?> AddAsync(Dto dto);
        Task<bool> DeleteAsync(int Id);
        Task<ResponseDto<Dto>?> EditAsync(Dto dto, int Id);
        Task<ResponseDto<List<Dto>>?> GetAllAsync();
        Task<ResponseDto<Dto>?> GetByIdAsync(int Id);
    }
}