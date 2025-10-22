using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.Reply;

namespace LinkUp.Core.Applicacion.ViewModel.PostCommen
{
    public class PostCommenViewModel : BasicViewModelForId
    {
        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
        public required string Text { get; set; }

        public List<ReplyViewModel>? ReplyCommen { get; set; } = new List<ReplyViewModel>();

    }
}
