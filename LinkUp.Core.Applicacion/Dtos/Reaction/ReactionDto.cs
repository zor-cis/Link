using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.Reaction
{
    public class ReactionDto : BasicDtoForId
    {
        public required int IdPublication { get; set; }
        public required int ReactionType { get; set; }
        public required string IdUser { get; set; }
    }
}
