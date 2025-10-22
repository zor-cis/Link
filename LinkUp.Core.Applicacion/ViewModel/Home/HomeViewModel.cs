using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
using LinkUp.Core.Applicacion.ViewModel.Reply;

namespace LinkUp.Core.Applicacion.ViewModel.Home
{
    public class HomeViewModel
    {
        public List<PublicationViewModel>? Publication { get; set; } = new List<PublicationViewModel>();

        public CreatePostCommenViewModel? CreateCommen { get; set; }
        public CreateReactionViewModel? CreateReaction { get; set; }
        public CreateReplyViewModel? CreateReply { get; set; }
        public ReactionViewModel? Reaction { get; set; }
    }
}
