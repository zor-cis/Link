using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Services;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IFriendshipRequestService : IGenericService<FriendshipRequestDto>
    {
        Task<ResponseDto<List<FriendshipRequestDto>>?> GetAllReceivedPendingRequests(string IdUser);
        Task<ResponseDto<List<FriendshipRequestDto>>?> GetAllSentFriendshipRequests(string IdUser);
        Task<ResponseDto<List<UserDto>>?> GetNonFriendsUsersAsync(string IdUser);
        Task<bool> UpdateFriendshipRequestStatusAsync(int idFriendshipRequest, int NewStatusFriendshipRequest);
    }
}