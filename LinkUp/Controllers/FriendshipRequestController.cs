using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.FriendshipRequest;
using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class FriendshipRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFriendshipRequestService _service;
        private readonly IMapper _mapper;
        public FriendshipRequestController(UserManager<AppUser> userManager, IFriendshipRequestService service, IMapper mapper)
        {
            _userManager = userManager;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        { 
            var userSession = await _userManager.GetUserAsync(User);
            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var sentRequests = await _service.GetAllSentFriendshipRequests(userSession.Id);
            var receivedRequest = await _service.GetAllReceivedPendingRequests(userSession.Id);

            var friendshipRequestVm = new FriendshipRequestViewModel
            {
                sentFriendships = _mapper.Map<List<SentFriendshipRequestViewModel>>(sentRequests!.Result),
                receivedFriendships = _mapper.Map<List<ReceivedFriendshipRequestViewModel>>(receivedRequest!.Result),
                updateFriendship = new UpdateFriendshipRequestStatusViewModel()
                {
                    idFriendshipRequest = 0,
                    NewStatusFriendshipRequest = (int)FriendshipRequestStatus.Pending
                },
            };
            return View("Index", friendshipRequestVm);
        }
        
        public async Task<IActionResult> NewFriend()
        {
            var userSession = await _userManager.GetUserAsync(User);
            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var nonFriendsUser = await _service.GetNonFriendsUsersAsync(userSession.Id);
            var FriendshipRequesVm = new NewFriendshipRequestViewModel
            {
                nonFriend = _mapper.Map<List<NonFriendsViewModel>>(nonFriendsUser!.Result),
                createFriendship = new CreateFriendshipRequestViewModel()
                {
                    Id = 0,
                    IdUserRequester = userSession.Id,
                    IdUserAddressee = "",
                    CreatedAt = DateTime.Now,
                    FriendshipRequestStatus = (int)FriendshipRequestStatus.Pending
                }
            };
            return View("NewFriend", FriendshipRequesVm);

        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(CreateFriendshipRequestViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("NewFriend", vm);
            }

            FriendshipRequestDto dto = _mapper.Map<FriendshipRequestDto>(vm);
            var result = await _service.AddAsync(dto);
            if (result == null || result.IsError)
            {
                TempData["ErrorMessage"] = result?.MessageResult ?? "Error desconocido al enviar solicitud de amistad.";
                return RedirectToRoute(new { controller = "FriendshipRequest", action = "NewFriend" });
            }
            return RedirectToRoute(new { controller = "FriendshipRequest", action = "Index" });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFriendship(UpdateFriendshipRequestStatusViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }
            var result = await _service.UpdateFriendshipRequestStatusAsync(vm.idFriendshipRequest, vm.NewStatusFriendshipRequest);
            return RedirectToRoute(new { controller = "FriendshipRequest", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFriendship(DeleteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Friend", action = "Index" });
            }

            await _service.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }



    }
}
