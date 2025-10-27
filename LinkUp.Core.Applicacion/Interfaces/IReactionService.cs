using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Services;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IReactionService : IGenericService<ReactionDto>
    {
        Task<ResponseDto<ReactionDto>?> GetByPublicationtAsync(int IdPublication);
    }
}
