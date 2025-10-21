using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.PostCommen
{
    public class CreatePostCommenViewModel : BasicViewModelForId
    {
        public required string IdUser { get; set; }
        public required int IdPublication { get; set; }
        public required string Text { get; set; }
    }
}
