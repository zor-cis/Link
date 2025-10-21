using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.PostCommen
{
    public class PostCommenDto : BasicDtoForId
    {
        public required string IdUser { get; set; }
        public required int IdPublication { get; set; }
        public required string Text { get; set; }
    }
}
