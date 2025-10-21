using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.Publication
{
    public class CreatePublicationViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre de la publicacion")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }

        [DataType(DataType.Url)]
        public string? VideoUrl { get; set; }

        [Required(ErrorMessage = "Es necesario seleccionar el tipo de publicacion")]
        public required int PublicationType { get; set; }

        public required string UserId { get; set; }
        public required DateTime CreateAt { get; set; }
    }
}
