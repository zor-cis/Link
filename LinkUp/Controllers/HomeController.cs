using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Home;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
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
        private readonly IPostCommenService _postCommenService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IPublicationService servicePublications, IMapper mapper, IPostCommenService postCommenService)
        {
            _logger = logger;
            _userManager = userManager;
            _servicePublications = servicePublications;
            _mapper = mapper;
            _postCommenService = postCommenService;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var publications = await _servicePublications.GetAllAsync();
            var homeView = new HomeViewModel
            {
                Publication = _mapper.Map<List<PublicationViewModel>>(publications!.Result),
                CreateCommen = new CreatePostCommenViewModel
                {
                    IdUser = userSession.Id,
                    IdPublication = 0,
                    Id = 0,
                    Text = ""
                },
            };

            foreach (var publication in homeView.Publication) 
            {
                var comments = await _postCommenService.GetAllByPublicationAsync(publication.Id);
                if(comments != null && !comments.IsError)
                {
                    publication.PostCommen = _mapper.Map<List<PostCommenViewModel>>(comments.Result);
                }
            }
            return View("Index", homeView);
        }

        
    }
}
