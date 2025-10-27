using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class FriendshipRequestService : GenericService<FriendshipRequest, FriendshipRequestDto>, IFriendshipRequestService
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepo;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IMapper _mapper;

        public FriendshipRequestService(IGenericRepository<FriendshipRequest> repo, IMapper mapper, IFriendshipRequestRepository friendshipRequestRepo, IAccountServiceForWebApp userService) : base(repo, mapper)
        {
            _friendshipRequestRepo = friendshipRequestRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ResponseDto<List<UserDto>>?> GetNonFriendsUsersAsync(string IdUser)
        {
            var response = new ResponseDto<List<UserDto>>();

            try
            {
                var AllUser = await _userService.GetAllUsersAsync(true);

                if (AllUser == null || AllUser.Count == 0)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontraron usuarios para enviar solicitudes";
                    return response;
                }

                var friendship = await _friendshipRequestRepo.GetQuery().Where(f => f.IdUserRequester == IdUser || f.IdUserAddressee == IdUser).ToListAsync();

                var friendsIds = friendship.Where(f => f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted).Select(f => f.IdUserRequester == IdUser ? f.IdUserAddressee : f.IdUserRequester).ToHashSet();

                var nonFriendsUsers = AllUser.Where(u => u.Id != IdUser && !friendsIds.Contains(u.Id)).ToList();

                response.IsError = false;
                response.Result = _mapper.Map<List<UserDto>>(nonFriendsUsers);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<List<FriendshipRequestDto>>?> GetAllReceivedPendingRequests(string IdUser)
        {
            var response = new ResponseDto<List<FriendshipRequestDto>>();

            try
            {
                var pandingRequests = await _friendshipRequestRepo.GetQuery().Where(f => f.IdUserAddressee == IdUser && f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Pending).ToListAsync();

                if (pandingRequests == null || pandingRequests.Count == 0)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontraron solicitudes de amistad pendientes";
                    return response;
                }

                var dtos = _mapper.Map<List<FriendshipRequestDto>>(pandingRequests);

                foreach (var dto in dtos)
                {
                    var user = await _userService.GetUserById(dto.IdUserRequester);
                    if (user != null)
                    {
                        dto.NameUserFriend = user.UserName;
                        dto.ImageProfileFriend = user.ProfileImage;
                    }
                }

                response.IsError = false;
                response.Result = dtos;

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<List<FriendshipRequestDto>>?> GetAllSentFriendshipRequests(string IdUser)
        {
            var response = new ResponseDto<List<FriendshipRequestDto>>();

            try
            {
                var pandingRequests = await _friendshipRequestRepo.GetQuery().Where(f => f.IdUserRequester == IdUser).ToListAsync();

                if (pandingRequests == null || pandingRequests.Count == 0)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontraron solicitudes de amistad pendientes";
                    return response;
                }

                var dtos = _mapper.Map<List<FriendshipRequestDto>>(pandingRequests);

                foreach (var dto in dtos)
                {
                    var user = await _userService.GetUserById(dto.IdUserAddressee);
                    if (user != null)
                    {
                        dto.NameUserFriend = user.UserName;
                        dto.ImageProfileFriend = user.ProfileImage;
                    }

                    var userFriends = await _friendshipRequestRepo.GetQuery().Where(f => (f.IdUserRequester == IdUser || f.IdUserAddressee == IdUser) && f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted).ToListAsync();

                    var recipientFriends = await _friendshipRequestRepo.GetQuery().Where(f => (f.IdUserRequester == dto.IdUserAddressee || f.IdUserAddressee == dto.IdUserAddressee) && f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted).ToListAsync();

                    var commonCount = recipientFriends.Count(r => userFriends.Any(u => (u.IdUserRequester == IdUser ? u.IdUserAddressee : u.IdUserRequester) == (r.IdUserRequester == dto.IdUserAddressee ? r.IdUserAddressee : r.IdUserRequester)));

                    dto.CommonFriendsCount = commonCount;

                }

                response.IsError = false;
                response.Result = dtos;

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<bool> UpdateFriendshipRequestStatusAsync(int idFriendshipRequest, int NewStatusFriendshipRequest)
        {
            try
            {
                var friendshipRequest = await _friendshipRequestRepo.GetById(idFriendshipRequest);

                if (friendshipRequest != null)
                {
                    friendshipRequest.FriendshipRequestStatus = NewStatusFriendshipRequest;
                    await _friendshipRequestRepo.UpdateAsync(idFriendshipRequest, friendshipRequest);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
