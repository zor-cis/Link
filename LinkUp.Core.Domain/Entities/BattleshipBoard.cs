using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class BattleshipBoard : BasicEnetityForId
    {
        public required string UserId { get; set; }
        public required int GameId { get; set; }

        public BattleshipGame? Game { get; set; }

        public ICollection<Cell>? Cells { get; set; }
        public ICollection<Ship>? Ships { get; set; }
        public ICollection<Attack>? Attacks { get; set; }
    }
}
