using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.User;
using LinkUp.Helpers;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountServiceForWebApp _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public LoginController(ILogger<HomeController> logger, IAccountServiceForWebApp service, UserManager<AppUser> userManager, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession != null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new LoginViewModel() { Password = "", UserName = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession != null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid) 
            {
                vm.Password = "";
                return View(vm);
            }

            LoginResponseDto? login = await _service.AuthenticaAsync(new LoginDto()
            {
                UserName = vm.UserName,
                Password = vm.Password
            });

            if (login != null && !login.HasError) 
            {
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }

            TempData["ErrorMessage"] = login?.MessageError ?? "Error desconocido al iniciar sesion.";
            vm.Password = "";
            return View(vm);
        }

        public async Task<IActionResult> Logout() 
        { 
            await _service.SingOutAsync();
            return RedirectToRoute(new { controller = "Login", action = "Index" });

        }

        public IActionResult Register()
        {

            return View(new RegisterUserViewModel() { Name = "", LastName = "", UserName = "", Email = "", Password = "", ConfirmPassword = "", PhoneNumber = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Password = "";
                vm.ConfirmPassword = "";
                return View(vm);
            }

            string origin = Request?.Headers?.Origin.ToString() ?? string.Empty;
            SaveUserDto newUser = _mapper.Map<SaveUserDto>(vm);
            
            newUser.ProfileImage = "";

            RegisterResponseDto? returnUser = await _service.RegisterUserAsync(newUser, origin);

            if(returnUser == null || returnUser!.HasError) 
            {
                TempData["ErrorMessage"] = returnUser?.MessageError ?? "Error desconocido al registrar el usuario.";
                return View(vm);
            }


          if(returnUser != null && !string.IsNullOrWhiteSpace(returnUser.Id)) 
          {
                newUser.Id = returnUser.Id;
                newUser.ProfileImage = FileManager.Upload(vm.ProfileImage, newUser.Id, "Users");
                await _service.EditUserAsync(newUser, origin, true);
          } 
           
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _service.ConfirmAccountAsync(userId, token);
            return View("ConfirmEmail", response);
        }




        public async Task<IActionResult> AccessDenied()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession != null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


    }
}
