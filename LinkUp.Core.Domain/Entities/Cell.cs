using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Cell : BasicEnetityForId
    {
        public required int BoardId { get; set; }
        public BattleshipBoard? Board { get; set; }

        public int? X { get; set; }
        public int? Y { get; set; }

        public required int CellState { get; set; }

    }
}
