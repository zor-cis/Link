using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.Reaction
{
    public class CreateReactionViewModel : BasicViewModelForId
    {
        public required int IdPublication { get; set; }
        public required int ReactionType { get; set; }
        public required string IdUser { get; set; }
    }
}
