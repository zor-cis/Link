using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.User;
using LinkUp.Helpers;
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
            
            var viewUser = new ProfileViewModel { UserProfile = _mapper.Map<UserViewModel>(userDto), EditUser = _mapper.Map<EditUserViewModel>(userDto)};
            return View("Index", viewUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel vm) 
        {
            if (!ModelState.IsValid) 
            {
                vm.Password = "";
                vm.ConfirmPassword = "";
                View("Index", vm);
            }

            string origin = Request.Headers.Origin.ToString() ?? string.Empty;
            SaveUserDto userDto = _mapper.Map<SaveUserDto>(vm);

            var UserNameCurrent = User!.Identity!.Name!;
            var currentDto = await _service.GetUserByUserName(UserNameCurrent);
            string? currentImagePath = "";

            if(currentDto != null) 
            {
                currentImagePath = currentDto.ProfileImage;
            }

            userDto.ProfileImage = FileManager.Upload(vm.ProfileImage, userDto.Id, "Users", true);
            var edit = await _service.EditUserAsync(userDto, origin);

            var newEdit = new ProfileViewModel { EditUser = _mapper.Map<EditUserViewModel>(edit) };
            return View("Index", newEdit);
        }

    }
}
