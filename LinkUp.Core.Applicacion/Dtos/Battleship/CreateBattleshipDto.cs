using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.Reply
{
    public class CreateBattleshipDto : BasicDtoForId
    {
        public required string Player1Id { get; set; }
        public required string Player2Id { get; set; }

        public required DateTime CreateAt { get; set; }
    }
}
