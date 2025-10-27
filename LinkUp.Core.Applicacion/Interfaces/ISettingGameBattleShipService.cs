using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Dtos.User;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface ISettingGameBattleShipService
    {
        Task<ResponseDto<List<UserDto>>> GetFriendsForBattleShipGame(string UserId);
        Task<ResponseDto<UserDto>> FindUserByName(string UserName);
        Task<ResponseDto<ResponseCreateBattleshipDto>> CreateNewGameAsync(CreateBattleshipDto dto, string currentUser);
    }
}