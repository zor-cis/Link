using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.PostCommen
{
    public class PostCommenViewModel : BasicViewModelForId
    {
        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
        public required string Text { get; set; }
    }
}
