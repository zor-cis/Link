using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.FriendshipRequest
{
    public class FriendshipRequestViewModel
    {
        public List<SentFriendshipRequestViewModel>? sentFriendships { get; set; }
        public List<ReceivedFriendshipRequestViewModel>? receivedFriendships { get; set; }
        public UpdateFriendshipRequestStatusViewModel? updateFriendship {  get; set; }
        public DeleteViewModel? deleteFriendships { get; set; }
    }
}
