using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Response;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IFriendService
    {
        Task<ResponseDto<List<FriendshipRequestDto>>?> GetFriendsAsync(string IdUser);
        Task<ResponseDto<List<PublicationDto>>?> GetPublicationsByFriends(string IdUser);
    }
}