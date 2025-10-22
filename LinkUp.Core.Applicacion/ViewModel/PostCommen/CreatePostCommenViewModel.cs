using LinkUp.Core.Applicacion.ViewModel.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.PostCommen
{
    public class CreatePostCommenViewModel : BasicViewModelForId
    {
        public required string IdUser { get; set; }
        public required int IdPublication { get; set; }

        [Required(ErrorMessage = "Debe ingresar el comentario")]
        [DataType(DataType.Text)]
        public required string Text { get; set; }
    }
}
