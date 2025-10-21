using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly IAccountServiceForWebApp _serviceUser;
        private readonly IPublicationService _service;
        private readonly IMapper _mapper;
        public PublicationController(IAccountServiceForWebApp serviceUser, IPublicationService service, IMapper mapper)
        {
            _serviceUser = serviceUser;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            var UserName = User.Identity?.Name;
            var UserDto = await _serviceUser.GetUserByUserName(UserName!);

            return View("CreatePublication", new CreatePublicationViewModel 
            { 
                Id = 0, 
                UserId = UserDto!.Id, 
                CreateAt = DateTime.Now, 
                PublicationType = 0, 
                Name = "",
                VideoUrl = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePublicationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePublication", vm);
            }

            var publicationDto = _mapper.Map<PublicationDto>(vm);
            publicationDto.ImageUrl = "";

            var publication = await _service.AddAsync(publicationDto);

            if (publication == null || publication.IsError)
            {
                TempData["ErrorMessage"] = publication?.MessageResult ?? "Error desconocido al crear publicación.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (vm.ImageUrl != null)
            {
                publicationDto.ImageUrl = FileManager.Upload(vm.ImageUrl, publication.Result!.Id.ToString(), "Publications", true);
                await _service.EditAsync(publicationDto, publication.Result!.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


    }
}
