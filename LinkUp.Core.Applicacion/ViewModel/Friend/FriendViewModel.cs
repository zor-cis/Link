using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.Friend
{
    public class FriendViewModel : BasicViewModelForId
    {
        public required string IdUserRequester { get; set; }
        public required string IdUserAddressee { get; set; }
        public required int FriendshipRequestStatus { get; set; }
        public required DateTime CreatedAt { get; set; }


        public required string NameUserFriend { get; set; }
        public required string ImageProfileFriend { get; set; }
    }
}
