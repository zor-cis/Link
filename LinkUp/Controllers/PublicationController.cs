using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Helpers;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublicationService _service;
        private readonly IMapper _mapper;
        public PublicationController(UserManager<AppUser> userManager, IPublicationService service, IMapper mapper)
        {
            _userManager = userManager;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            return View("CreatePublication", new CreatePublicationViewModel 
            { 
                Id = 0, 
                UserId = currentUser.Id, 
                CreateAt = DateTime.Now, 
                PublicationType = 0, 
                Name = "",
                VideoUrl = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePublicationViewModel vm)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("CreatePublication", vm);
            }

            vm.UserId = currentUser.Id;
            PublicationDto dto = _mapper.Map<PublicationDto>(vm);
            dto.ImageUrl = "";

            var returnpublication = await _service.AddAsync(dto);

            if (returnpublication == null || returnpublication.IsError)
            {
                TempData["ErrorMessage"] = returnpublication?.MessageResult ?? "Error desconocido al crear publicación.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (returnpublication != null && vm.PublicationType == (int)PublicationType.Imagen)
            {
                dto.Id = returnpublication.Result!.Id;
                dto.ImageUrl = FileManager.Upload(vm.ImageUrl, dto.Id.ToString(), "Publications");
                await _service.EditAsync(dto, dto.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


    }
}
