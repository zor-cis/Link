using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.FriendshipRequest
{
    public class FriendshipRequestDto : BasicDtoForId
    {
        public required string IdUserRequester { get; set; }
        public required string IdUserAddressee { get; set; }
        public required int FriendshipRequestStatus { get; set; }
        public required DateTime CreatedAt { get; set; }


        public required string NameUserFriend { get; set; }
        public required string ImageProfileFriend { get; set; }
        public required int CommonFriendsCount { get; set; }
    }
}
