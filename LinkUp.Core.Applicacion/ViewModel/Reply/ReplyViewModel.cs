using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.Reply
{
    public class ReplyViewModel : BasicViewModelForId
    {
        public required string ReplyComment { get; set; }

        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
    }
}
