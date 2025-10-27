using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class PostCommenController : Controller
    {
        private readonly IPostCommenService _service;
        private readonly IMapper _mapper;
        public PostCommenController(IPostCommenService service, IMapper mapper)
        {
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
