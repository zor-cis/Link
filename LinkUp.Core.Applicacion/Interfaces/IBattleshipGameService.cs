using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Response;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IBattleshipGameService
    {
        Task<bool> CheckBothBoardsReadyAsync(int gameId);
        Task<ResponseDto<EnterBattleshipDto>> EnterGameAsync(int gameId, string userId);
        Task<bool> GetBoardStatusAsync(int boardId);
        Task<ResponseDto<List<ConfiguredShipDto>>> GetConfiguredBoardAsync(int boardId);
        Task<ResponseDto<List<ActiveGameBattleshipDto>>> GetPenddingGameAsync(string userId);
        Task<ResponseDto<List<PendingShipDto>>> GetPendingShipsAsync(int boardId);
        Task<ResponseDto<List<WonGameBattleshipDto>>> GetWonGameAsync(string userId);
        Task<bool> IsBoardReadyAsync(int boardId);
        Task<bool> PlaceShipAsync(PlaceShipDto dto);
    }
}