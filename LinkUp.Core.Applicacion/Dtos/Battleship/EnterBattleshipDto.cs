namespace LinkUp.Core.Applicacion.Dtos.Battleship
{
    public class EnterBattleshipDto
    {
        public required int GameId { get; set; }            
        public required int BoardId { get; set; }            
        public required bool IsConfigurationPhase { get; set; }
    }
}
