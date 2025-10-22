using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.Reply
{
    public class ReplyDto : BasicDtoForId
    {
        public required string IdUser { get; set; }
        public required int IdPostComment { get; set; }
        public required string ReplyComment { get; set; }
    }
}
