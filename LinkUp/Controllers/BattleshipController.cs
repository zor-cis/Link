using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.BattleShip;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class BattleshipController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISettingGameBattleShipService _settingGame;
        private readonly IBattleshipGameService _game;

        public BattleshipController(UserManager<AppUser> userManager, IMapper mapper, ISettingGameBattleShipService settingGame, IBattleshipGameService game)
        {
            _userManager = userManager;
            _mapper = mapper;
            _settingGame = settingGame;
            _game = game;

        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var gamePendding = await _game.GetPenddingGameAsync(userSession.Id);
            var gameWon = await _game.GetWonGameAsync(userSession.Id);

            var battleShipIndexView = new BattleShipIndexViewModel
            {
                ActiveGames = _mapper.Map<List<ActiveGameBattleshipViewModel>>(gamePendding.Result),
                WonGames = _mapper.Map<List<WonGameBattleshipViewModel>>(gameWon.Result)
            };

            return View("Index", battleShipIndexView);
        }

        public async Task<IActionResult> NewGame()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var friendsForGame = await _settingGame.GetFriendsForBattleShipGame(userSession.Id);

            var newGameView = new NewGameViewModel
            {
                Friends = _mapper.Map<List<FriendsForBattleShipGameViewModel>>(friendsForGame.Result),
                createGame = new CreateBattleshipViewModel() 
                { 
                    Id = 0,
                    Player1Id = userSession.Id,
                    Player2Id = "",
                    CreateAt = DateTime.UtcNow
                }
            };

            return View("NewGame", newGameView);
        }


        [HttpPost]
        public async Task<IActionResult> NewGame(CreateBattleshipViewModel vm)
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("NewGame", vm);
            }

            CreateBattleshipDto dto = _mapper.Map<CreateBattleshipDto>(vm);
            var resultDto = await _settingGame.CreateNewGameAsync(dto, userSession.Id);

            if (resultDto == null || resultDto.IsError)
            {
                TempData["ErrorMessage"] = resultDto?.MessageResult ?? "Error desconocido al crear juego.";
                return RedirectToRoute(new { controller = "Battleship", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Battleship", action = "Index" });
        }


    }
}
