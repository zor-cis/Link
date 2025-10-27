using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class SettingGameBattleShipService : ISettingGameBattleShipService
    {
        private readonly IFriendshipRequestRepository _friendsRepo;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IBattleshipGameRepository _battleshipGameRepo;
        private readonly IBattleshipBoardRepository _battleshipBoardRepo;
        private readonly IMapper _Mapper;

        public SettingGameBattleShipService(IFriendshipRequestRepository friendsRepo, IAccountServiceForWebApp userService, IMapper mapper, IBattleshipGameRepository battleshipGameRepo, IBattleshipBoardRepository battleshipBoardRepo)
        {
            _friendsRepo = friendsRepo;
            _userService = userService;
            _Mapper = mapper;
            _battleshipGameRepo = battleshipGameRepo;
            _battleshipBoardRepo = battleshipBoardRepo;
        }

        public async Task<ResponseDto<List<UserDto>>> GetFriendsForBattleShipGame(string UserId)
        {
            var response = new ResponseDto<List<UserDto>>();
            try
            {
                var AllUser = await _userService.GetAllUsersAsync(true);

                if (AllUser == null || AllUser.Count == 0)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontraron amigos";
                    return response;
                }

                var friendShip = await _friendsRepo.GetQuery().Where(f => (f.IdUserRequester == UserId || f.IdUserAddressee == UserId) && f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted).ToListAsync();

                var friendIds = friendShip.Select(f => f.IdUserRequester == UserId ? f.IdUserAddressee : f.IdUserAddressee).Distinct().ToList();

                var friends = AllUser.Where(u => friendIds.Contains(u.Id)).ToList();

                response.IsError = false;
                response.Result = _Mapper.Map<List<UserDto>>(friends);

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ResponseDto<UserDto>> FindUserByName(string UserName) 
        {
            var response = new ResponseDto<UserDto>();
            try
            {
                var AllUser = await _userService.GetAllUsersAsync(true);
                var User = AllUser.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower());

                if(User == null) 
                {
                    response.IsError = true;
                    response.MessageResult = "Usuario no encontrado";
                }
                response.IsError = false;
                response.Result = _Mapper.Map<UserDto>(User);

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ResponseDto<ResponseCreateBattleshipDto>> CreateNewGameAsync(CreateBattleshipDto dto, string currentUser) 
        {
            var response = new ResponseDto<ResponseCreateBattleshipDto>();
            try
            {
                if (string.IsNullOrEmpty(dto.Player1Id) || string.IsNullOrEmpty(dto.Player2Id)) 
                { 
                    response.IsError = true;
                    response.MessageResult = "Solo se puede jugar con dos personas";
                    return response;
                }

                if (string.IsNullOrEmpty(currentUser) || (currentUser != dto.Player1Id && currentUser != dto.Player2Id))
                {
                    response.IsError = true;
                    response.MessageResult = "Usuario actual invalido para esta partida";
                    return response;
                }

                var isInGameYet = await _battleshipGameRepo.GetQuery().AnyAsync(g => ((g.Player1Id == dto.Player1Id && g.Player2Id == dto.Player2Id) || (g.Player1Id == dto.Player2Id && g.Player2Id == dto.Player1Id)) &&
                  (g.GameStatus == (int)GameStatus.InProgress || g.GameStatus == (int)GameStatus.Pending || g.GameStatus == (int)GameStatus.SettingUp));
                
                if (isInGameYet) 
                { 
                    response.IsError = true;
                    response.MessageResult = "Ya existe una partida en curso entre estos jugadores";
                    return response;
                }

                var NewGame = _Mapper.Map<BattleshipGame>(dto);
                await _battleshipGameRepo.AddAsync(NewGame);

                var boardPlayer1 = new BattleshipBoard
                {
                    Id = 0,
                    GameId = NewGame.Id,
                    UserId = dto.Player1Id
                };
                
                var boardPlayer2 = new BattleshipBoard
                {
                    Id = 0,
                    GameId = NewGame.Id,
                    UserId = dto.Player2Id
                };
                await _battleshipBoardRepo.AddRangeAsync(new List<BattleshipBoard> { boardPlayer1, boardPlayer2 });

                var resultBoardPlayer = currentUser == dto.Player1Id ? boardPlayer1 : boardPlayer2;

                response.IsError = false;
                response.Result = new ResponseCreateBattleshipDto
                {
                    GameId = NewGame.Id,
                    BoardIdPlayer = resultBoardPlayer.Id
                };

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }
   
    }


}
