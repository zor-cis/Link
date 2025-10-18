using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.User;
using LinkUp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountServiceForWebApp _service;
        private readonly IMapper _mapper;

        public UserController(ILogger<HomeController> logger, IAccountServiceForWebApp service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;
            if(userName == null) 
            { 
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var userDto = await _service.GetUserByUserName(userName);
            var viewUser = new ProfileViewModel { UserProfile = _mapper.Map<UserViewModel>(userDto)};
            return View("Index", viewUser);
        }

    }
}
