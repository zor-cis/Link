using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Services;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IPostCommenService : IGenericService<PostCommenDto>
    {
        Task<ResponseDto<List<PostCommenDto>>?> GetAllByPublicationAsync(int IdPublication);
    }
}
