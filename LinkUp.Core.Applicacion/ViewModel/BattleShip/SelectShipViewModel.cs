namespace LinkUp.Core.Applicacion.ViewModel.BattleShip
{
    public class SelectShipViewModel
    {
        public required int GameId { get; set; }
        public required int BoardId { get; set; }
        public required bool IsConfigurationPhase { get; set; }
        public List<PendingShipViewModel> PendingShips { get; set; } = new();
        public int? SelectedShipSize { get; set; } 
    }
}
