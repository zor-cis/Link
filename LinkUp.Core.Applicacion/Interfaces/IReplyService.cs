using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Services;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IReplyService : IGenericService<ReplyDto>
    {
      Task<ResponseDto<List<ReplyDto>>?> GetAllByCommentAsync(int IdComment);
    }
}
