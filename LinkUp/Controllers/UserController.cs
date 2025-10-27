using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.User;
using LinkUp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IAccountServiceForWebApp _service;
        private readonly IMapper _mapper;

        public UserController(IAccountServiceForWebApp service, IMapper mapper)
        {
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
            var username = User.Identity?.Name;
            var currenDto = await _service.GetUserByUserName(username!);
            if (currenDto == null)
            {
                TempData["ErrorMessage"] = "No se pudo encontrar el usuario actual.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Password = "";
                vm.ConfirmPassword = "";
                return View("Index", new ProfileViewModel
                { 
                        UserProfile = _mapper.Map<UserViewModel>(currenDto), 
                        EditUser = vm
                });
            }

            var origin = Request.Headers.Origin.ToString() ?? string.Empty;
            var userDto = _mapper.Map<SaveUserDto>(vm);

            if (vm.ProfileImage != null)
            {
                userDto.ProfileImage = FileManager.Upload(vm.ProfileImage, userDto.Id, "Users", true);
            }
            else 
            { 
                userDto.ProfileImage = currenDto.ProfileImage;
            }

            var result = await _service.EditUserAsync(userDto, origin);
            if (result.HasError) 
            { 
                TempData["ErrorMessage"] = result.MessageError ?? "Error desconocido";
                return View("Index", new ProfileViewModel
                {
                    UserProfile = _mapper.Map<UserViewModel>(currenDto),
                    EditUser = vm
                });
            }

            var updateUser = await _service.GetUserByUserName(result.UserName);
            return View("Index", new ProfileViewModel
            {
                UserProfile = _mapper.Map<UserViewModel>(updateUser),
                EditUser = _mapper.Map<EditUserViewModel>(updateUser)
            });
        }

        public async Task<IActionResult> Delete(string Id) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error en la solicitud";
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var dto = await _service.GetUserById(Id);
            if(dto == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado";
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            DeleteUserViewModel vm = new DeleteUserViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName
            };

            return View("Delete", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel vm)
        {
            if (!ModelState.IsValid) 
            { 
                return View("Delete", vm);
            }

            await _service.DeleteAsync(vm.Id);
            FileManager.Delete(vm.Id, "Users");

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
