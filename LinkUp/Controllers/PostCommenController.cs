using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class PostCommenController : Controller
    {
        private readonly IAccountServiceForWebApp _serviceUser;
        private readonly IPostCommenService _service;
        private readonly IMapper _mapper;
        public PostCommenController(IAccountServiceForWebApp serviceUser, IPostCommenService service, IMapper mapper)
        {
            _serviceUser = serviceUser;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostCommenViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePublication", vm);
            }

            PostCommenDto dto = _mapper.Map<PostCommenDto>(vm);
            var resultDto = await _service.AddAsync(dto);

            if (resultDto == null || resultDto.IsError)
            {
                TempData["ErrorMessage"] = resultDto?.MessageResult ?? "Error desconocido al crear publicación.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


    }
}
