namespace LinkUp.Core.Applicacion.Dtos.Battleship
{
    public class PlaceShipDto
    {
        public required int BoardId { get; set; }        
        public required int ShipSize { get; set; }       
        public required int StartX { get; set; }         
        public required int StartY { get; set; }         
        public required int Direction { get; set; }
    }
}
