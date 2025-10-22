using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Reply : BasicEnetityForId
    {
        public required string IdUser { get; set; }
        public required int IdPostComment { get; set; }
        public PostCommen? PostCommen { get; set; }
        public required string ReplyComment { get; set; }
    }
}
