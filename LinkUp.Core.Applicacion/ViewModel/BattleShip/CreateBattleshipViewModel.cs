using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.BattleShip
{
    public class CreateBattleshipViewModel : BasicViewModelForId
    {
        public required string Player1Id { get; set; }
        public required string Player2Id { get; set; }

        public required DateTime CreateAt { get; set; }
    }
}
