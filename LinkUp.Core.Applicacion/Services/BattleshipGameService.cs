using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Battleship;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class BattleshipGameService : IBattleshipGameService
    {
        private readonly IFriendshipRequestRepository _friendsRepo;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IBattleshipGameRepository _battleshipGameRepo;
        private readonly IBattleshipBoardRepository _battleshipBoardRepo;
        private readonly IShipRepository _ship;
        private readonly IMapper _Mapper;

        public BattleshipGameService(IFriendshipRequestRepository friendsRepo, IAccountServiceForWebApp userService, IMapper mapper, IBattleshipGameRepository battleshipGameRepo, IBattleshipBoardRepository battleshipBoardRepo, IShipRepository ship)
        {
            _friendsRepo = friendsRepo;
            _userService = userService;
            _Mapper = mapper;
            _battleshipGameRepo = battleshipGameRepo;
            _battleshipBoardRepo = battleshipBoardRepo;
            _ship = ship;
        }

        public async Task<ResponseDto<List<WonGameBattleshipDto>>> GetWonGameAsync(string userId)
        {
            var response = new ResponseDto<List<WonGameBattleshipDto>>();

            try
            {
                var wonGames = await _battleshipGameRepo.GetQuery()
                    .Where(g => g.WinnerId == userId && g.GameStatus == (int)GameStatus.Completed)
                    .ToListAsync();

                var dtos = _Mapper.Map<List<WonGameBattleshipDto>>(wonGames);

                var winnerUser = await _userService.GetUserById(userId);
                var winnerName = winnerUser!.UserName;

                for (int i = 0; i < dtos.Count; i++)
                {
                    var originalGame = wonGames[i];

                    var opponentId = originalGame.Player1Id == userId ? originalGame.Player2Id : originalGame.Player1Id;
                    var opponentUser = await _userService.GetUserById(opponentId);
                    var opponentName = opponentUser!.UserName;

                    var duration = originalGame.EndDate.HasValue ? originalGame.EndDate.Value - originalGame.StartDate : TimeSpan.Zero;

                    dtos[i].WinnerName = winnerName;
                    dtos[i].OponentName = opponentName;
                    dtos[i].EndDate = originalGame.EndDate ?? DateTime.MinValue;
                    dtos[i].Duration = duration;
                }

                response.IsError = false;
                response.Result = dtos;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<ActiveGameBattleshipDto>>> GetPenddingGameAsync(string userId)
        {
            var response = new ResponseDto<List<ActiveGameBattleshipDto>>();

            try
            {
                var games = await _battleshipGameRepo.GetQuery().Where(g => (g.Player1Id == userId || g.Player2Id == userId) && (g.GameStatus == (int)GameStatus.SettingUp || g.GameStatus == (int)GameStatus.InProgress)).ToListAsync();

                var dtos = _Mapper.Map<List<ActiveGameBattleshipDto>>(games);

                for (int i = 0; i < dtos.Count; i++)
                {
                    var originalGame = games[i];
                    var opponentId = originalGame.Player1Id == userId ? originalGame.Player2Id : originalGame.Player1Id;
                    var opponentUser = await _userService.GetUserById(opponentId);
                    var opponentName = opponentUser!.UserName;

                    dtos[i].OponnetName = opponentName;
                    dtos[i].IsConfigurationPhase = originalGame.GameStatus == (int)GameStatus.SettingUp;
                }

                response.IsError = false;
                response.Result = dtos;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<EnterBattleshipDto>> EnterGameAsync(int gameId, string userId)
        {
            var response = new ResponseDto<EnterBattleshipDto>();

            try
            {
                var game = await _battleshipGameRepo.GetQuery().Include(g => g.Boards).FirstOrDefaultAsync(g => g.Id == gameId);

                if (game == null || game.GameStatus == (int)GameStatus.Completed)
                {
                    response.IsError = true;
                    response.MessageResult = "Partida no encontrada o ya finalizada";
                    return response;
                }

                var board = game.Boards?.FirstOrDefault(b => b.UserId == userId);
                if (board == null)
                {
                    response.IsError = true;
                    response.MessageResult = "Tablero no encontrado para el usuario en esta partida";
                    return response;
                }

                var dto = _Mapper.Map<EnterBattleshipDto>(board);
                dto.GameId = game.Id;
                dto.IsConfigurationPhase = game.GameStatus == (int)GameStatus.SettingUp;

                response.Result = dto;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<PendingShipDto>>> GetPendingShipsAsync(int boardId)
        {
            var response = new ResponseDto<List<PendingShipDto>>();

            try
            {
                var ships = await _battleshipBoardRepo.GetQuery().Include(b => b.Ships).Where(b => b.Id == boardId).SelectMany(b => b.Ships!).Where(s => !s.IsPlaced).ToListAsync();

                response.Result = _Mapper.Map<List<PendingShipDto>>(ships);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
            }

            return response;
        }

        public async Task<bool> PlaceShipAsync(PlaceShipDto dto)
        {
            try
            {
                var board = await _battleshipBoardRepo.GetQuery()
                    .Include(b => b.Ships)
                    .FirstOrDefaultAsync(b => b.Id == dto.BoardId);

                if (board == null || board.Ships == null)
                {
                    return false;
                }

                var ship = board.Ships.FirstOrDefault(s => s.Size == dto.ShipSize && !s.IsPlaced);
                if (ship == null)
                {
                    return false;
                }

                var occupiedCells = board.Ships
                    .Where(s => s.IsPlaced)
                    .SelectMany(s => GetShipCells(s.StartX!.Value, s.StartY!.Value, s.Size, s.Direction))
                    .ToHashSet();

                var newShipCells = GetShipCells(dto.StartX, dto.StartY, dto.ShipSize, dto.Direction);

                if (!AreCellsValid(newShipCells) || newShipCells.Any(c => occupiedCells.Contains(c)))
                {
                    return false;
                }

                _Mapper.Map(dto, ship);
                ship.IsPlaced = true;

                await _ship.UpdateAsync(ship.Id, ship);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsBoardReadyAsync(int boardId)
        {
            var board = await _battleshipBoardRepo.GetQuery().Include(b => b.Ships).FirstOrDefaultAsync(b => b.Id == boardId);

            if (board == null || board.Ships == null || board.Ships.Count == 0)
            {
                return false;
            }

            return board.Ships.All(s => s.IsPlaced);
        }

        public async Task<bool> CheckBothBoardsReadyAsync(int gameId)
        {
            var game = await _battleshipGameRepo.GetQuery().Include(g => g.Boards!).ThenInclude(b => b.Ships).FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null || game.Boards == null || game.Boards.Count != 2)
            {
                return false;
            }

            var bothReady = game.Boards.All(b => b.Ships != null && b.Ships.All(s => s.IsPlaced));
            if (bothReady && game.GameStatus == (int)GameStatus.SettingUp)
            {
                game.GameStatus = (int)GameStatus.InProgress;
                await _battleshipGameRepo.UpdateAsync(game.Id, game);
            }

            return bothReady;
        }

        public async Task<ResponseDto<List<ConfiguredShipDto>>> GetConfiguredBoardAsync(int boardId)
        {
            var response = new ResponseDto<List<ConfiguredShipDto>>();

            try
            {
                var ships = await _battleshipBoardRepo.GetQuery()
                    .Include(b => b.Ships)
                    .Where(b => b.Id == boardId)
                    .SelectMany(b => b.Ships!)
                    .Where(s => s.IsPlaced)
                    .ToListAsync();

                response.Result = _Mapper.Map<List<ConfiguredShipDto>>(ships);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
            }

            return response;
        }

        public async Task<bool> GetBoardStatusAsync(int boardId)
        {
            var board = await _battleshipBoardRepo.GetQuery()
                .Include(b => b.Ships)
                .FirstOrDefaultAsync(b => b.Id == boardId);

            if (board == null || board.Ships == null || board.Ships.Count == 0)
            {
                return false;
            }

            return board.Ships.All(s => s.IsPlaced);
        }





        private List<(int x, int y)> GetShipCells(int startX, int startY, int size, int direction)
        {
            var cells = new List<(int x, int y)>();
            for (int i = 0; i < size; i++)
            {
                int x = startX, y = startY;
                switch (direction)
                {
                    case (int)Direction.Up: y -= i; break;
                    case (int)Direction.Down: y += i; break;
                    case (int)Direction.Left: x -= i; break;
                    case (int)Direction.Right: x += i; break;
                }
                cells.Add((x, y));
            }
            return cells;
        }

        private bool AreCellsValid(List<(int x, int y)> cells)
        {
            return cells.All(c => c.x >= 0 && c.x < 12 && c.y >= 0 && c.y < 12);
        }



    }
}
