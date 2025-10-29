using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Ship : BasicEnetityForId
    {
        public required int BoardId { get; set; }
        public BattleshipBoard? Board { get; set; }


        public required int Size { get; set; }
        public int? StartX { get; set; }
        public int? StartY { get; set; }

        public int? Direction { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsSunk { get; set; }

    }
}
