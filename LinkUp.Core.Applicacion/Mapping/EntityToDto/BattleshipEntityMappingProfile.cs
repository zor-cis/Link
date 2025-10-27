using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class BattleshipEntityMappingProfile : Profile
    {
        public BattleshipEntityMappingProfile() 
        {
            CreateMap<CreateBattleshipDto, BattleshipGame>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.GameStatus, opt => opt.Ignore())
                .ForMember(dest => dest.TurnStatus, opt => opt.Ignore())
                .ForMember(dest => dest.WinnerId, opt => opt.Ignore())
                .ForMember(dest => dest.EndDate, opt => opt.Ignore())
                .ForMember(dest => dest.Boards, opt => opt.Ignore());

            CreateMap<BattleshipGame, ResponseCreateBattleshipDto>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BoardIdPlayer, opt => opt.Ignore()); 

            CreateMap<BattleshipGame, WonGameBattleshipDto>()
                .ForMember(dest => dest.WinnerName, opt => opt.Ignore())
                .ForMember(dest => dest.OponentName, opt => opt.Ignore())
                .ForMember(dest => dest.Duration, opt => opt.Ignore());

            CreateMap<BattleshipGame, ActiveGameBattleshipDto>()
                .ForMember(dest => dest.OponnetName, opt => opt.Ignore())
                .ForMember(dest => dest.IsConfigurationPhase, opt => opt.Ignore());

            CreateMap<Ship, PendingShipDto>()
                .ForMember(dest => dest.ShipSize, opt => opt.MapFrom(src => src.Size));

            CreateMap<AttackDto, Attack>()
                .ForMember(dest => dest.AttackResult, opt => opt.Ignore())
                .ForMember(dest => dest.TargetBoard, opt => opt.Ignore());

            CreateMap<PlaceShipDto, Ship>()
                .ForMember(dest => dest.IsPlaced, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.IsSunk, opt => opt.Ignore())
                .ForMember(dest => dest.Board, opt => opt.Ignore());
        }
    }
}
