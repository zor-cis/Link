using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.ViewModel.BattleShip;


namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class BattleshipViewModelMappingProfile : Profile
    {
        public BattleshipViewModelMappingProfile() 
        {
            CreateMap<WonGameBattleshipDto, WonGameBattleshipViewModel>()
               .ReverseMap();
             
            CreateMap<ActiveGameBattleshipDto, ActiveGameBattleshipViewModel>()
               .ReverseMap();

            CreateMap<CreateBattleshipDto, CreateBattleshipViewModel>()
               .ReverseMap();

            CreateMap<PlaceShipViewModel, PlaceShipDto>()
                .ReverseMap();

            CreateMap<PendingShipDto, PendingShipViewModel>()
                .ReverseMap();
        }
    }
}
