using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
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
            return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            ReactionDto dto = _mapper.Map<ReactionDto>(vm);

            var resultDto = await _service.AddAsync(dto);

            if (resultDto == null || resultDto.IsError)
            {
                TempData["ErrorMessage"] = resultDto?.MessageResult ?? "Error desconocido al crear publicación.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel vm) 
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", vm);
            }

            await _service.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


    }
}
