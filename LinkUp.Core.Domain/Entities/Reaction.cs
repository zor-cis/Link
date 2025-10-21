using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Reaction : BasicEnetityForId
    {
        public required int IdPublication { get; set; }
        public Publication? Publication { get; set; }
        public required int ReactionType { get; set; }
        public required string IdUser { get; set; }

    }
}
