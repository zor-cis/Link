using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Attack : BasicEnetityForId
    {
        public required int GameId { get; set; }

        public required int TargetBoardId { get; set; }
        public BattleshipBoard? TargetBoard { get; set; }

        public required string AttackerId { get; set; }
        public required string TargetId { get; set; }

        public int? X { get; set; }
        public int? Y { get; set; }

        public required int AttackResult { get; set; }


    }
}
