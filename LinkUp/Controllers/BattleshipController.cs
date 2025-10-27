using AutoMapper;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.BattleShip;
using LinkUp.Core.Applicacion.ViewModel.Friend;
using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
using LinkUp.Core.Applicacion.ViewModel.Reply;
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

        public BattleshipController(UserManager<AppUser> userManager, IMapper mapper, ISettingGameBattleShipService settingGame)
        {
            _userManager = userManager;
            _mapper = mapper;
            _settingGame = settingGame;

        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            return View("Index");
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
                Friends = _mapper.Map<List<FriendsForBattleShipGameViewModel>>(friendsForGame.Result)
            };

            return View("NewGame", newGameView);
        }

        
    }
}
