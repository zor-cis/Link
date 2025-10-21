using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class PostCommen : BasicEnetityForId
    {
        public required string IdUser { get; set; }
        public required int IdPublication { get; set; }
        public Publication? Publication {  get; set; }
        public required string Text { get; set; }
    }
}
