using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class FriendService : IFriendService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IMapper _mapper;

        public FriendService(IPublicationRepository publicationRepository, IFriendshipRequestRepository friendshipRequestRepository, IMapper mapper, IAccountServiceForWebApp userService)
        {
            _publicationRepository = publicationRepository;
            _friendshipRequestRepository = friendshipRequestRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PublicationDto>>?> GetPublicationsByFriends(string IdUser)
        {
            var response = new ResponseDto<List<PublicationDto>>();
            try
            {
                var friendships = await _friendshipRequestRepository.GetQuery().Where(f => (f.IdUserRequester == IdUser || f.IdUserAddressee == IdUser) && f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted).ToListAsync();

                var friendIds = friendships.Select(f => f.IdUserRequester == IdUser ? f.IdUserAddressee : f.IdUserRequester).ToList();

                var publications = await _publicationRepository.GetQuery().Where(p => friendIds.Contains(p.UserId)).ToListAsync();

                var publicationDtos = _mapper.Map<List<PublicationDto>>(publications);

                response.IsError = false;
                response.Result = publicationDtos;

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<List<FriendshipRequestDto>>?> GetFriendsAsync(string IdUser)
        {
            var response = new ResponseDto<List<FriendshipRequestDto>>();

            try
            {
                var friendships = await _friendshipRequestRepository.GetQuery()
                    .Where(f =>
                        (f.IdUserRequester == IdUser || f.IdUserAddressee == IdUser) &&
                        f.FriendshipRequestStatus == (int)FriendshipRequestStatus.Accepted)
                    .ToListAsync();

                if (friendships == null || friendships.Count == 0)
                {
                    response.IsError = true;
                    response.MessageResult = "No se encontraron amigos";
                    return response;
                }

                var dtos = _mapper.Map<List<FriendshipRequestDto>>(friendships);

                foreach (var dto in dtos)
                {
                    var friendId = dto.IdUserRequester == IdUser ? dto.IdUserAddressee : dto.IdUserRequester;
                    var user = await _userService.GetUserById(friendId);

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
            }

            return response;
        }


    }
}
