namespace LinkUp.Core.Applicacion.Dtos.Battleship
{
    public class AttackDto
    {
        public required int GameId { get; set; }
        public required int TargetBoardId { get; set; }
        public required string AttackerId { get; set; }
        public required string TargetId { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
