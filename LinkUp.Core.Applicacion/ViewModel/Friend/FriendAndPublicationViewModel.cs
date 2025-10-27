using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
using LinkUp.Core.Applicacion.ViewModel.Reply;

namespace LinkUp.Core.Applicacion.ViewModel.Friend
{
    public class FriendAndPublicationViewModel
    {
        public List<PublicationViewModel>? Publication { get; set; } = new List<PublicationViewModel>();
        public List<FriendViewModel>? Friends { get; set; } = new List<FriendViewModel>();

        public CreatePostCommenViewModel? CreateCommen { get; set; }
        public CreateReactionViewModel? CreateReaction { get; set; }
        public CreateReplyViewModel? CreateReply { get; set; }
        public ReactionViewModel? Reaction { get; set; }

        public DeleteViewModel? DeleteView { get; set; }
    }
}
