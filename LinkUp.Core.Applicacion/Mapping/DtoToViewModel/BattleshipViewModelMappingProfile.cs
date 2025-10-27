using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.ViewModel.BattleShip;
using LinkUp.Core.Applicacion.ViewModel.FriendshipRequest;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class BattleshipViewModelMappingProfile : Profile
    {
        public BattleshipViewModelMappingProfile() 
        {
            CreateMap<WonGameBattleshipDto, WonGameBattleshipViewModel>()
               .ReverseMap();
             
            CreateMap<ActiveGameBattleshipDto, ActiveGameBattleshipDto>()
               .ReverseMap();

            CreateMap<CreateBattleshipDto, CreateBattleshipViewModel>()
               .ReverseMap();
        }
    }
}
