using System.Diagnostics;
using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class ReactionController : Controller
    {
        private readonly IAccountServiceForWebApp _serviceUser;
        private readonly IReactionService _service;
        private readonly IMapper _mapper;
        public ReactionController(IAccountServiceForWebApp serviceUser, IReactionService service, IMapper mapper)
        {
            _serviceUser = serviceUser;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePublication", vm);
            }

            var UserName = User!.Identity!.Name;
            var UserDto = await _serviceUser.GetUserByUserName(UserName!);

            ReactionDto dto = _mapper.Map<ReactionDto>(vm);
            dto.IdUser = UserDto!.Id;
            dto.IdPublication = vm.IdPublication;

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
