namespace LinkUp.Core.Applicacion.ViewModel.BattleShip
{
    public class ActiveGameBattleshipViewModel
    {
        public required int GameId { get; set; }
        public required string OponnetName { get; set; }
        public required DateTime StartDate { get; set; }
        public required bool IsConfigurationPhase { get; set; }
    }
}
