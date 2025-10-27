using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class FriendshipRequest : BasicEnetityForId
    {
        public required string IdUserRequester { get; set; } //Envia
        public required string IdUserAddressee { get; set; } //Recibe
        public required int FriendshipRequestStatus { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
