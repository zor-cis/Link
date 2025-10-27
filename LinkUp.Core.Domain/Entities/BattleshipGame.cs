using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class BattleshipGame : BasicEnetityForId
    {
        public required string Player1Id { get; set; }
        public required string Player2Id { get; set; }

        public required int GameStatus { get; set; }
        public required int TurnStatus { get; set; }

        public string? WinnerId {  get; set; }

        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<BattleshipBoard>? Boards { get; set; }
    }
}
