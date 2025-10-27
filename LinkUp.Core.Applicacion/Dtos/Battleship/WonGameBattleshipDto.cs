namespace LinkUp.Core.Applicacion.Dtos.Battleship
{
    public class WonGameBattleshipDto
    {
        public required int GameId { get; set; }
        public required string WinnerName { get; set; }
        public required string OponentName { get; set; }
        public required DateTime EndDate { get; set; }
        public required TimeSpan Duration { get; set; }

    }
}
