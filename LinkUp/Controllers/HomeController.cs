using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Home;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublicationService _servicePublications;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IPublicationService servicePublications, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _servicePublications = servicePublications;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var publications = await _servicePublications.GetAllAsync();
            var homeView = new HomeViewModel { Publication = _mapper.Map<List<PublicationViewModel>>(publications!.Result) };
            return View("Index", homeView);
        }

        
    }
}
