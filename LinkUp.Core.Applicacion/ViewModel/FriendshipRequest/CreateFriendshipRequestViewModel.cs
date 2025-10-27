using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.FriendshipRequest
{
    public class CreateFriendshipRequestViewModel : BasicViewModelForId
    {
        public required string IdUserRequester { get; set; }
        public required string IdUserAddressee { get; set; }
        public required int FriendshipRequestStatus { get; set; }
        public required DateTime CreatedAt { get; set; }

    }
}
