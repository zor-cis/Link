using LinkUp.Core.Applicacion.ViewModel.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.Reply
{
    public class CreateReplyViewModel : BasicViewModelForId
    {
        public required string IdUser { get; set; }
        public required int IdPostComment { get; set; }

        [Required(ErrorMessage = "Debe ingresar la respuesta")]
        [DataType(DataType.Text)]
        public required string ReplyComment { get; set; }
    }
}
