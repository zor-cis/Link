using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.PostCommen
{
    public class PostCommenViewModel : BasicViewModelForId
    {
        public required string NameUser { get; set; }
        public required string Text { get; set; }
    }
}
