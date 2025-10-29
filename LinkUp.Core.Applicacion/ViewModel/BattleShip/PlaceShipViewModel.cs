using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkUp.Core.Applicacion.ViewModel.BattleShip
{
    public class PlaceShipViewModel
    {
        public int BoardId { get; set; }
        public int ShipSize { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Direction { get; set; } 
        public int GameId { get; set; }
    }
}
