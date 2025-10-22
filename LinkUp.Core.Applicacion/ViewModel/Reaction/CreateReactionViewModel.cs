using LinkUp.Core.Applicacion.ViewModel.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.Reaction
{
    public class CreateReactionViewModel : BasicViewModelForId
    {
        public required int IdPublication { get; set; }

        [Required(ErrorMessage = "Indique si le gusto o no")]
        public required int ReactionType { get; set; }
        public required string IdUser { get; set; }
    }
}
