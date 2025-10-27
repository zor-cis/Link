using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkUp.Core.Applicacion.ViewModel.BattleShip
{
    public class BattleShipIndexViewModel
    {
        public List<ActiveGameBattleshipViewModel>? ActiveGames { get; set; }
        public List<WonGameBattleshipViewModel>? WonGames { get; set; }

    }
}
